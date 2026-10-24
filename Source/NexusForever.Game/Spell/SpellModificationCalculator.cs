using System.Reflection;
using NexusForever.Game.Static.Spell;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell
{
    /// <summary>
    /// Changes spell and effect values with Spell4Modification and Spell4EffectModification rows the same way the client does, see 0x1403D4210 and 0x1403D4910 in the client.
    /// </summary>
    public static class SpellModificationCalculator
    {
        public readonly record struct Modification(uint Parameter, uint Priority, SpellModificationType Type, float Data00, float Data01, float Data02, float Data03)
        {
            public static Modification From(Spell4ModificationEntry entry)
                => new(entry.ModificationParameterEnum, entry.Priority, (SpellModificationType)entry.ModificationTypeEnum, entry.Data00, entry.Data01, entry.Data02, entry.Data03);

            public static Modification From(Spell4EffectModificationEntry entry)
                => new(entry.ModificationParameterEnum, entry.Priority, (SpellModificationType)entry.ModificationTypeEnum, entry.Data00, entry.Data01, entry.Data02, entry.Data03);
        }

        // parameters whose changes are summed per priority, the rest are bit fields applied one after another or a single value
        private const uint SpellSummedParameters  = 0x31BF6Fu;
        private const uint EffectSummedParameters = 0x30FDCu;

        private static readonly MethodInfo memberwiseClone = typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// Return the spell value of the supplied parameter changed by the modifications.
        /// </summary>
        public static float CalculateSpellValue(IReadOnlyCollection<Modification> modifications, SpellModificationParameter parameter, float value)
        {
            uint id = (uint)parameter;
            bool summed     = id < 0x16u && ((SpellSummedParameters >> (int)id) & 1u) != 0u;
            bool sequential = id is 14u or 17u or 18u;
            return Calculate(modifications, id, value, summed, sequential);
        }

        /// <summary>
        /// Return a copy of the <see cref="Spell4EffectsEntry"/> changed by the modifications, the entry itself if nothing changes.
        /// </summary>
        /// <remarks>
        /// TODO: parameters 0 to 3 and 16 to 18 are fields of the client's effect copy the server doesn't know the names of.
        /// </remarks>
        public static Spell4EffectsEntry ApplyToEffect(Spell4EffectsEntry entry, IReadOnlyCollection<Modification> modifications, SpellEffectTypeEntry effectType)
        {
            if (modifications.Count == 0)
                return entry;

            var copy = (Spell4EffectsEntry)memberwiseClone.Invoke(entry, null);
            foreach (uint id in modifications.Select(m => m.Parameter).Distinct())
            {
                bool summed     = id < 0x12u && ((EffectSummedParameters >> (int)id) & 1u) != 0u;
                bool sequential = id is 0u or 1u or 5u;

                switch (id)
                {
                    case (uint)SpellEffectModificationParameter.DurationTime:
                        copy.DurationTime = ToUInt(Calculate(modifications, id, entry.DurationTime, summed, sequential));
                        break;
                    case (uint)SpellEffectModificationParameter.Flags:
                        copy.Flags = (SpellEffectFlags)ToUInt(Calculate(modifications, id, (uint)entry.Flags, summed, sequential));
                        break;
                    case >= (uint)SpellEffectModificationParameter.DataBits00 and <= (uint)SpellEffectModificationParameter.DataBits09:
                    {
                        int index = (int)id - (int)SpellEffectModificationParameter.DataBits00;
                        uint dataType = GetDataType(effectType, index);
                        float value = Calculate(modifications, id, FromDataBits(GetDataBits(entry, index), dataType), summed, sequential);
                        SetDataBits(copy, index, ToDataBits(value, dataType));
                        break;
                    }
                }
            }

            return copy;
        }

        private static float Calculate(IReadOnlyCollection<Modification> modifications, uint parameter, float baseValue, bool summed, bool sequential)
        {
            // the client keeps the rows sorted by priority with the clamp rows after the others of the same priority
            List<Modification> rows = modifications
                .Where(m => m.Parameter == parameter)
                .OrderBy(m => m.Priority)
                .ThenBy(m => m.Type == SpellModificationType.Clamp ? 1 : 0)
                .ToList();
            if (rows.Count == 0)
                return baseValue;

            if (summed)
            {
                // each priority's changes are summed and clamped, then added before the next priority
                float current = baseValue;
                foreach (IGrouping<uint, Modification> priority in rows.GroupBy(m => m.Priority))
                {
                    float change = 0f;
                    foreach (Modification row in priority)
                    {
                        if (row.Type == SpellModificationType.Clamp)
                        {
                            change = Operation(row, baseValue, change);
                            break;
                        }

                        change += Operation(row, baseValue, current);
                    }

                    current += change;
                }

                return current;
            }

            if (sequential)
            {
                float current = baseValue;
                foreach (Modification row in rows)
                    current = Operation(row, baseValue, current);
                return current;
            }

            return Operation(rows[0], baseValue, baseValue);
        }

        /// <summary>
        /// See 0x1403D6D70 and 0x1403D6F20 in the client, the change and clamp operations return a change, the others a value.
        /// </summary>
        private static float Operation(Modification row, float baseValue, float current)
        {
            switch (row.Type)
            {
                case SpellModificationType.Change:
                {
                    float change = (row.Data00 - 1f) * baseValue + row.Data01;
                    if ((int)row.Data03 == 0)
                        return MathF.Min(MathF.Max(change, 0f), MathF.Max(row.Data02 - current, 0f));
                    return MathF.Max(MathF.Min(change, 0f), MathF.Min(row.Data02 - current, 0f));
                }
                case SpellModificationType.ChangeOfCurrent:
                {
                    float change = (row.Data00 - 1f) * current;
                    if ((int)row.Data02 == 0)
                        return MathF.Min(MathF.Max(change, 0f), MathF.Max(row.Data01 - current, 0f));
                    return MathF.Max(MathF.Min(change, 0f), MathF.Min(row.Data01 - current, 0f));
                }
                case SpellModificationType.Clamp:
                    return (int)row.Data01 == 0 ? MathF.Min(row.Data00, current) : MathF.Max(row.Data00, current);
                case SpellModificationType.Set:
                    return row.Data00;
                case SpellModificationType.Bits:
                {
                    uint bits = (uint)row.Data00;
                    return (int)row.Data01 == 0 ? (uint)current & ~bits : (uint)current | bits;
                }
                default:
                    return 0f;
            }
        }

        private static uint ToUInt(float value)
        {
            return value <= 0f ? 0u : (uint)MathF.Round(value);
        }

        private static uint GetDataType(SpellEffectTypeEntry entry, int index)
        {
            if (entry == null)
                return 2u;

            return index switch
            {
                0 => entry.DataType00,
                1 => entry.DataType01,
                2 => entry.DataType02,
                3 => entry.DataType03,
                4 => entry.DataType04,
                5 => entry.DataType05,
                6 => entry.DataType06,
                7 => entry.DataType07,
                8 => entry.DataType08,
                _ => entry.DataType09
            };
        }

        // SpellEffectType.tbl field types: 0 and 3 float, 1 int, 2 uint
        private static float FromDataBits(uint bits, uint dataType)
        {
            return dataType switch
            {
                0u or 3u => BitConverter.UInt32BitsToSingle(bits),
                1u       => (int)bits,
                _        => bits
            };
        }

        private static uint ToDataBits(float value, uint dataType)
        {
            return dataType switch
            {
                0u or 3u => BitConverter.SingleToUInt32Bits(value),
                1u       => (uint)(int)MathF.Round(value),
                _        => ToUInt(value)
            };
        }

        private static uint GetDataBits(Spell4EffectsEntry entry, int index)
        {
            return index switch
            {
                0 => entry.DataBits00,
                1 => entry.DataBits01,
                2 => entry.DataBits02,
                3 => entry.DataBits03,
                4 => entry.DataBits04,
                5 => entry.DataBits05,
                6 => entry.DataBits06,
                7 => entry.DataBits07,
                8 => entry.DataBits08,
                _ => entry.DataBits09
            };
        }

        private static void SetDataBits(Spell4EffectsEntry entry, int index, uint value)
        {
            switch (index)
            {
                case 0: entry.DataBits00 = value; break;
                case 1: entry.DataBits01 = value; break;
                case 2: entry.DataBits02 = value; break;
                case 3: entry.DataBits03 = value; break;
                case 4: entry.DataBits04 = value; break;
                case 5: entry.DataBits05 = value; break;
                case 6: entry.DataBits06 = value; break;
                case 7: entry.DataBits07 = value; break;
                case 8: entry.DataBits08 = value; break;
                default: entry.DataBits09 = value; break;
            }
        }
    }
}
