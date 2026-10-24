using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Combat;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Combat;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Spell;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;
using NexusForever.Network.World.Combat;

namespace NexusForever.Game.Combat
{
    public sealed class DamageCalculator : IDamageCalculator
    {
        #region Dependency Injection

        private readonly ILogger<DamageCalculator> log;
        private readonly IGameTableManager gameTableManager;
        private readonly IDamageDescription damageDescription;

        public DamageCalculator(
            ILogger<DamageCalculator> log,
            IGameTableManager gameTableManager,
            IDamageDescription damageDescription)
        {
            this.log               = log;
            this.gameTableManager  = gameTableManager;
            this.damageDescription = damageDescription;
        }

        #endregion

        /// <summary>
        /// Returns the calculated damage and updates the referenced <see cref="ISpellTargetEffectInfo"/> appropriately.
        /// </summary>
        /// <remarks>
        /// TODO: This should probably return an instance of a Class which describes all the damage done to both entities. Attackers can have reflected damage from this, etc.
        /// </remarks>
        public void CalculateDamage(ISpellExecutionContext executionContext, IUnitEntity victim, ISpellTargetEffectInfo info)
        {
            IUnitEntity attacker = executionContext.Spell.Caster;

            damageDescription.DamageType   = info.Entry.DamageType;
            damageDescription.CombatResult = CombatResult.Hit;

            var castData = new CombatLogCastData
            {
                CasterId     = attacker.Guid,
                TargetId     = victim.Guid,
                SpellId      = executionContext.Spell.Parameters.SpellInfo.Entry.Id, // TODO: This was updated in order to use ISpell, check if correct
                CombatResult = damageDescription.CombatResult
            };

            if (victim.CombatEffectManager.IsInvulnerable)
            {
                executionContext.AddCombatLog(new CombatLogImmune
                {
                    CastData = castData
                });
                return;
            }

            // ForcedAction effects on the attacker take priority over those on the victim
            ForcedOutcome? forcedOutcome = attacker.CombatEffectManager.ConsumeForcedOutcome(false)
                ?? victim.CombatEffectManager.ConsumeForcedOutcome(true);

            if (forcedOutcome is ForcedOutcome.Deflect or ForcedOutcome.Miss
                || forcedOutcome == null && CalculateDeflect(attacker, victim))
            {
                damageDescription.CombatResult = CombatResult.Avoid;
                executionContext.AddCombatLog(new CombatLogDeflect
                    {
                        BMultiHit = false,
                        CastData  = castData
                    });
                return;
            }

            uint damage = CalculateBaseDamage(attacker, victim, info.Entry);

            // distributed damage is split between every target of the effect
            if (info.Entry.EffectType == SpellEffectType.DistributedDamage)
            {
                int targets = executionContext.TargetCollection.GetTargets(info.Entry.TargetFlags).Count();
                if (targets > 1)
                    damage /= (uint)targets;
            }

            damageDescription.RawDamage       = damage;
            damageDescription.RawScaledDamage = damage;

            damage = CalculateBaseDamageVariance(damage);

            damage = GetDamageAfterArmorMitigation(victim, info.Entry.DamageType, damage);

            // TODO: Add in other attacking modifiers like Armor Pierce, Strikethrough, Multi-Hit, etc.

            if (CalculateCrit(ref damage, attacker, victim, forcedOutcome == ForcedOutcome.Critical))
                damageDescription.CombatResult = CombatResult.Critical;

            uint preGlanceDamage = damage;
            if (CalculateGlance(ref damage, attacker, victim))
            {
                uint glanceDamage = preGlanceDamage - damage;
                // TODO: Add CombatLog
            }

            // PersonalDmgHealMod effects on either unit
            DamageType damageType = info.Entry.DamageType;
            damage = (uint)MathF.Round(damage
                * attacker.CombatEffectManager.GetDamageDealtMultiplier(victim, damageType)
                * victim.CombatEffectManager.GetDamageTakenMultiplier(attacker, damageType));
            uint reduction = victim.CombatEffectManager.GetDamageTakenReduction(attacker);
            damage = damage > reduction ? damage - reduction : 0u;

            if (info.Entry.EffectType == SpellEffectType.DamageShields)
            {
                // only shields take the damage
                damageDescription.ShieldAbsorbAmount = victim.CombatEffectManager.IsShieldOverloaded ? 0u : Math.Min(damage, victim.Shield);
                damageDescription.AdjustedDamage     = 0u;
                info.AddDamage(damageDescription);
                return;
            }

            uint absorbedAmount = victim.CombatEffectManager.AbsorbDamage(damageType, damage);
            damageDescription.AbsorbedAmount = absorbedAmount;
            damage -= absorbedAmount;

            uint shieldedAmount = victim.CombatEffectManager.IsShieldOverloaded ? 0u : CalculateShieldAmount(damage, victim);
            if (shieldedAmount > damage)
            {
                damageDescription.ShieldAbsorbAmount = damage;
                damage = 0;
            }
            else
            {
                damageDescription.ShieldAbsorbAmount = shieldedAmount;
                damage -= shieldedAmount;
            }

            // TODO: Add in other defensive modifiers

            damageDescription.AdjustedDamage = damage;

            info.AddDamage(damageDescription);

            // TODO: Queue Proc Events*/
        }

        /// <summary>
        /// Calculates the heal and updates the referenced <see cref="ISpellTargetEffectInfo"/> appropriately.
        /// </summary>
        /// <remarks>
        /// The base amount is the client's effect magnitude (0x140565600), the same as damage.
        /// TODO: variance, crits and the healing multipliers aren't in the client, they follow the damage path and the property names.
        /// </remarks>
        public void CalculateHeal(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, DamageType damageType)
        {
            IUnitEntity caster = executionContext.Spell.Caster;

            damageDescription.DamageType   = damageType;
            damageDescription.CombatResult = CombatResult.Hit;

            uint heal = CalculateBaseDamage(caster, target, info.Entry);
            damageDescription.RawDamage       = heal;
            damageDescription.RawScaledDamage = heal;

            heal = CalculateBaseDamageVariance(heal);

            if (CalculateCrit(ref heal, caster, target))
                damageDescription.CombatResult = CombatResult.Critical;

            if (damageType == DamageType.Heal)
            {
                float multiplier = (caster.GetProperty(Property.HealingMultiplierOutgoing)?.Value ?? 1f)
                    * (target.GetProperty(Property.HealingMultiplierIncoming)?.Value ?? 1f)
                    * caster.CombatEffectManager.GetHealingDealtMultiplier(target)
                    * target.CombatEffectManager.GetHealingTakenMultiplier(caster);
                heal = (uint)MathF.Round(heal * MathF.Max(0f, multiplier));

                uint absorbedAmount = target.CombatEffectManager.AbsorbHealing(heal);
                damageDescription.AbsorbedAmount = absorbedAmount;
                heal -= absorbedAmount;

                uint missing = target.MaxHealth - Math.Min(target.Health, target.MaxHealth);
                damageDescription.OverkillAmount = heal > missing ? heal - missing : 0u;
            }
            else
            {
                uint missing = target.MaxShieldCapacity - Math.Min(target.Shield, target.MaxShieldCapacity);
                damageDescription.OverkillAmount = heal > missing ? heal - missing : 0u;
            }

            damageDescription.AdjustedDamage = heal;
            info.AddDamage(damageDescription);
        }

        /// <summary>
        /// Get base damage value for the given <see cref="IUnitEntity"/> with the provided parameter data from the <see cref="Spell4EffectsEntry"/>.
        /// </summary>
        private uint CalculateBaseDamage(IUnitEntity caster, IUnitEntity target, Spell4EffectsEntry entry)
        {
            return (uint)CalculateEffectMagnitude(caster, target, entry);
        }

        /// <summary>
        /// Return the magnitude of the <see cref="Spell4EffectsEntry"/> parameters cast by the <see cref="IUnitEntity"/> on the target, see 0x140565600 in the client.
        /// </summary>
        public float CalculateEffectMagnitude(IUnitEntity caster, IUnitEntity target, Spell4EffectsEntry entry)
        {
            float basePropertyDamage = CalculateBasePropertyDamage(caster, entry);
            float baseEntityDamage   = CalculateBaseEntityDamage(caster, target, entry);

            float typeMultiplier = 1f;
            float typeBaseDamage = 0;
            switch (entry.EffectType)
            {
                case SpellEffectType.Transference:
                {
                    typeMultiplier = BitConverter.UInt32BitsToSingle(entry.DataBits02);
                    typeBaseDamage = entry.DataBits03;
                    break;
                }
                case SpellEffectType.Damage:
                case SpellEffectType.Heal:
                case SpellEffectType.DistanceDependentDamage:
                case SpellEffectType.DistributedDamage:
                case SpellEffectType.HealShields:
                case SpellEffectType.DamageShields:
                {
                    typeMultiplier = BitConverter.UInt32BitsToSingle(entry.DataBits00);
                    typeBaseDamage = entry.DataBits01;
                    break;
                }
            }

            float typeDamage = (typeBaseDamage + baseEntityDamage) * typeMultiplier;
            if (caster.Type is not EntityType.Player and not EntityType.Ghost)
                ApplyNonPlayerScaling(caster, entry, ref basePropertyDamage, ref typeDamage);

            float baseDamage = basePropertyDamage + typeDamage;

            // damage effects can be reduced to nothing by the multiplier, other effects are never reduced below their base value
            float minimumMultiplier = entry.EffectType is SpellEffectType.Damage or SpellEffectType.Transference or SpellEffectType.DamageShields ? 0f : 1f;
            float propertyMultiplier = MathF.Max(minimumMultiplier, caster.GetProperty((Property)(entry.DamageType + 140))?.Value ?? 0f);

            return propertyMultiplier * baseDamage;
        }

        /// <summary>
        /// Scale the damage of a non player caster, the property damage has diminishing returns above the value expected for the caster's level.
        /// </summary>
        /// <remarks>
        /// See 0x140565600 in the client, the expected value is (level * Datafloat0 + Datafloat01) * the average stat coefficient of GameFormula 712.
        /// </remarks>
        private void ApplyNonPlayerScaling(IUnitEntity caster, Spell4EffectsEntry entry, ref float propertyDamage, ref float typeDamage)
        {
            GameFormulaEntry formulaEntry = gameTableManager.GameFormula.GetEntry(712);
            float levelMultiplier  = formulaEntry?.Datafloat0  ?? 70f;
            float levelBase        = formulaEntry?.Datafloat01 ?? 100f;
            float excessMultiplier = formulaEntry?.Datafloat02 ?? 1f;
            float divisor          = formulaEntry?.Datafloat03 ?? 2f;

            uint statParameters = 0u;
            float statCoefficients = 0f;
            for (int i = 0; i < 4; i++)
            {
                if (entry.ParameterType[i] is >= SpellEffectParameterType.Brutality and <= SpellEffectParameterType.Insight
                    or >= SpellEffectParameterType.Grit and <= SpellEffectParameterType.SupportPower)
                {
                    statParameters++;
                    statCoefficients += entry.ParameterValue[i];
                }
            }

            float expected = statParameters == 0u ? 0f : (caster.Level * levelMultiplier + levelBase) * MathF.Abs(statCoefficients / statParameters);
            if (propertyDamage <= expected)
                propertyDamage = divisor != 0f ? propertyDamage / divisor : 0f;
            else
            {
                float excess = expected != 0f ? (propertyDamage - expected) / expected : 0f;
                propertyDamage = ((1f - MathF.Exp(-excess)) * excessMultiplier + 1f) * (divisor != 0f ? expected / divisor : 0f);
            }

            typeDamage = divisor != 0f ? typeDamage / divisor : 0f;
        }

        private float CalculateBasePropertyDamage(IUnitEntity caster, Spell4EffectsEntry entry)
        {
            float GetProperty(Property property)
            {
                return caster.GetProperty(property)?.Value ?? 0f;
            }

            GameFormulaEntry forumulaEntry = gameTableManager.GameFormula.GetEntry(1266);

            float value = 0f;
            for (int i = 0; i < 4; i++)
            {
                float intermediateValue = 0f;
                switch (entry.ParameterType[i])
                {
                    case SpellEffectParameterType.Brutality:
                        intermediateValue = GetProperty(Property.Strength);
                        break;
                    case SpellEffectParameterType.Finesse:
                        intermediateValue = GetProperty(Property.Dexterity);
                        break;
                    case SpellEffectParameterType.Tech:
                        intermediateValue = GetProperty(Property.Technology);
                        break;
                    case SpellEffectParameterType.Moxie:
                        intermediateValue = GetProperty(Property.Magic);
                        break;
                    case SpellEffectParameterType.Insight:
                        intermediateValue = GetProperty(Property.Wisdom);
                        break;
                    case SpellEffectParameterType.Grit:
                        intermediateValue = GetProperty(Property.Stamina);
                        break;
                    // client defaults to a value of 0.25f if the game table entry is missing
                    case SpellEffectParameterType.AssaultPower:
                        intermediateValue = GetProperty(Property.AssaultRating) * (forumulaEntry?.Datafloat0 ?? 0.25f);
                        break;
                    case SpellEffectParameterType.SupportPower:
                        intermediateValue = GetProperty(Property.SupportRating) * (forumulaEntry?.Datafloat01 ?? 0.25f);
                        break;
                }

                value += intermediateValue * entry.ParameterValue[i];
            }

            // client rounds to the nearest integer, see 0x140566D40 in the client
            return MathF.Round(value, MidpointRounding.AwayFromZero);
        }

        private float CalculateBaseEntityDamage(IUnitEntity caster, IUnitEntity target, Spell4EffectsEntry entry)
        {
            float value = 0f;
            for (int i = 0; i < 4; i++)
            {
                float intermediateValue = 0f;
                switch (entry.ParameterType[i])
                {
                    case SpellEffectParameterType.TargetMaxHealth:
                        intermediateValue = target.MaxHealth;
                        break;
                    case SpellEffectParameterType.CasterMaxHealth:
                        intermediateValue = caster.MaxHealth;
                        break;
                    case SpellEffectParameterType.CasterShieldCapacity:
                        intermediateValue = caster.Shield;
                        break;
                    case SpellEffectParameterType.TargetShieldCapacity:
                        intermediateValue = target.Shield;
                        break;
                    case SpellEffectParameterType.CasterMaxShieldCapacity:
                        intermediateValue = caster.MaxShieldCapacity;
                        break;
                    case SpellEffectParameterType.TargetMaxShieldCapacity:
                        intermediateValue = target.MaxShieldCapacity;
                        break;
                    // client uses the caster's level for spell tooltips and the item's level for item tooltips, see 0x1404DE8D0 in the client
                    // TODO: use the item's level for spells cast by an item
                    case SpellEffectParameterType.ItemBudget:
                        intermediateValue = GetItemBudget(caster.Level);
                        // property modifiers scale the budget by the property's value per point, see 0x1404DE840 in the client
                        if (entry.EffectType == SpellEffectType.UnitPropertyModifier)
                            intermediateValue *= gameTableManager.UnitProperty2.GetEntry(entry.DataBits00)?.ValuePerPoint ?? 1f;
                        break;
                    case SpellEffectParameterType.TargetCurrentHealth:
                        intermediateValue = target.Health;
                        break;
                    case SpellEffectParameterType.TargetMissingHealth:
                        intermediateValue = (target.MaxHealth - target.Health);
                        break;
                    case SpellEffectParameterType.TargetMissingShields:
                        intermediateValue = (target.MaxShieldCapacity - target.Shield);
                        break;
                    case SpellEffectParameterType.CasterCurrentHealth:
                        intermediateValue = caster.Health;
                        break;
                    case SpellEffectParameterType.CasterMissingHealth:
                        intermediateValue = (caster.MaxHealth - caster.Health);
                        break;
                    case SpellEffectParameterType.CasterMissingShields:
                        intermediateValue = (caster.MaxShieldCapacity - caster.Shield);
                        break;
                    case SpellEffectParameterType.PerLevel:
                        intermediateValue = caster.Level;
                        break;
                }

                value += intermediateValue * entry.ParameterValue[i];
            }

            return value;
        }

        /// <summary>
        /// Return the item budget for the supplied item level.
        /// </summary>
        /// <remarks>
        /// See 0x14041F1B0 in the client, each <see cref="GameFormulaEntry"/> is a curve of Datafloat0 * e^(Datafloat01 * x) + Datafloat02 * x^Datafloat03 where x is the level above Dataint0.
        /// Levels below Dataint0 of entry 65 use entry 991, levels below Dataint0 of entry 253 use entry 65 and all others use entry 253.
        /// </remarks>
        private float GetItemBudget(uint itemLevel)
        {
            GameFormulaEntry formulaEntry;
            if (itemLevel < (gameTableManager.GameFormula.GetEntry(65)?.Dataint0 ?? 0u))
                formulaEntry = gameTableManager.GameFormula.GetEntry(991);
            else if (itemLevel < (gameTableManager.GameFormula.GetEntry(253)?.Dataint0 ?? 0u))
                formulaEntry = gameTableManager.GameFormula.GetEntry(65);
            else
                formulaEntry = gameTableManager.GameFormula.GetEntry(253);

            if (formulaEntry == null)
                return 0f;

            float x = itemLevel - formulaEntry.Dataint0;
            return formulaEntry.Datafloat0 * MathF.Pow(MathF.E, formulaEntry.Datafloat01 * x) + formulaEntry.Datafloat02 * MathF.Pow(x, formulaEntry.Datafloat03);
        }

        private uint CalculateBaseDamageVariance(uint damage)
        {
            return (uint)(damage * (Random.Shared.Next(95, 103) / 100f));
        }

        private uint GetDamageAfterArmorMitigation(IUnitEntity victim, DamageType damageType, uint damage)
        {
            // armor plus the damage type's resist is the rating and the two mitigation offsets the base, see 0x140464240 in the client
            Property resist;
            Property offset;
            switch (damageType)
            {
                case DamageType.Physical:
                    resist = Property.ResistPhysical;
                    offset = Property.DamageMitigationPctOffsetPhysical;
                    break;
                case DamageType.Tech:
                    resist = Property.ResistTech;
                    offset = Property.DamageMitigationPctOffsetTech;
                    break;
                case DamageType.Magic:
                    resist = Property.ResistMagic;
                    offset = Property.DamageMitigationPctOffsetMagic;
                    break;
                default:
                    return damage;
            }

            float rating = victim.GetPropertyValue(Property.Armor) + victim.GetPropertyValue(resist);
            float baseValue = victim.GetPropertyValue(Property.DamageMitigationPctOffset) + victim.GetPropertyValue(offset);
            float mitigation = MathF.Max(0f, GetRatingValue(victim, 1234u, rating, baseValue));

            return (uint)MathF.Round(damage * (1f - mitigation));
        }

        private bool IsSuccessfulChance(float percentage)
        {
            return new Random().Next(1, 10000) <= percentage * 10000f;
        }

        /// <summary>
        /// Calculates and returns the shielded amount of damage.
        /// </summary>
        private uint CalculateShieldAmount(uint damage, IUnitEntity victim)
        {
            uint maxShieldAmount = (uint)(damage * victim.GetPropertyValue(Property.ShieldMitigationMax));
            uint shieldedAmount = Math.Min(victim.Shield, maxShieldAmount);

            return shieldedAmount;
        }

        /// <summary>
        /// Returns whether this attack was deflected.
        /// </summary>
        /// <remarks>Calculates chance to deflect an attack, avoiding all damage from that attack.</remarks>
        private bool CalculateDeflect(IUnitEntity attacker, IUnitEntity victim)
        {
            // TODO: Add in Strikethrough Calculations (and that increases Armor Pierce)

            float deflectChance = GetRatingPercentMod(Property.RatingAvoidIncrease, victim);
            return IsSuccessfulChance(deflectChance);
        }

        /// <summary>
        /// Returns whether this attack crit, and if so, modifies the referenced damage value appropriately.
        /// </summary>
        private bool CalculateCrit(ref uint damage, IUnitEntity attacker, IUnitEntity victim, bool forceCrit = false)
        {
            // TODO: Add in Crit Deflect and Critical Mitigation calculations

            if (!forceCrit)
            {
                float critRate = GetRatingPercentMod(Property.RatingCritChanceIncrease, attacker);
                if (critRate <= 0f || !IsSuccessfulChance(critRate))
                    return false;
            }

            damage = (uint)Math.Round(damage * GetRatingPercentMod(Property.RatingCritSeverityIncrease, attacker));
            return true;
        }

        /// <summary>
        /// Returns whether this attack was glanced, and if so, modifies the referenced damage value appropriately.
        /// </summary>
        private bool CalculateGlance(ref uint damage, IUnitEntity attacker, IUnitEntity victim)
        {
            float glanceChance = GetRatingPercentMod(Property.RatingGlanceChance, victim);
            if (glanceChance <= 0f)
                return false;

            bool glance = IsSuccessfulChance(glanceChance);
            if (glance)
                damage = (uint)Math.Round((float)(damage * (1 - GetRatingPercentMod(Property.RatingGlanceAmount, victim))));

            return glance;
        }

        // base property and GameFormula of each rating, see the Lua Unit:Get*Chance functions (0x140460670 to 0x140465C40) in the client
        private static readonly Dictionary<Property, (Property Base, uint FormulaId)> ratings = new()
        {
            { Property.RatingCritChanceIncrease,   (Property.BaseCritChance,                1231u) },
            { Property.RatingCritSeverityIncrease, (Property.CriticalHitSeverityMultiplier, 1232u) },
            { Property.RatingAvoidReduce,          (Property.BaseAvoidReduceChance,         1230u) },
            { Property.RatingLifesteal,            (Property.BaseLifesteal,                 1233u) },
            { Property.RatingAvoidIncrease,        (Property.BaseAvoidChance,               1235u) },
            { Property.RatingCritChanceDecrease,   (Property.BaseAvoidCritChance,           1236u) },
            { Property.RatingMultiHitChance,       (Property.BaseMultiHitChance,            1240u) },
            { Property.RatingDamageReflectChance,  (Property.BaseDamageReflectChance,       1241u) },
            { Property.RatingCriticalMitigation,   (Property.BaseCriticalMitigation,        1242u) },
            { Property.RatingIntensity,            (Property.BaseIntensity,                 1243u) },
            { Property.RatingVigor,                (Property.BaseVigor,                     1244u) },
            { Property.RatingGlanceChance,         (Property.BaseGlanceChance,              1245u) },
            { Property.RatingArmorPierce,          (Property.IgnoreArmorBase,               1269u) },
            { Property.RatingMultiHitAmount,       (Property.BaseMultiHitAmount,            1270u) },
            { Property.RatingGlanceAmount,         (Property.BaseGlanceAmount,              1271u) },
            { Property.RatingDamageReflectAmount,  (Property.BaseDamageReflectAmount,       1272u) },
        };

        /// <summary>
        /// Return the value of the supplied rating <see cref="Property"/> for the <see cref="IUnitEntity"/>, its base property included.
        /// </summary>
        private float GetRatingPercentMod(Property property, IUnitEntity entity)
        {
            if (!ratings.TryGetValue(property, out (Property Base, uint FormulaId) rating))
            {
                log.LogWarning($"Unhandled Property in calculating Percentage from Rating: {property}");
                return 0f;
            }

            return GetRatingValue(entity, rating.FormulaId, entity.GetPropertyValue(property), entity.GetPropertyValue(rating.Base));
        }

        /// <summary>
        /// Return the value of a rating, see 0x140453B60 in the client.
        /// </summary>
        /// <remarks>
        /// Each point of rating is worth Datafloat0 / level * Datafloat01 percent on top of Dataint0 until the percentage reaches Datafloat02.
        /// Rating past that is scaled by cos(excess / (excess + Datafloat03) * pi / 2). The result is added to the base and capped at Dataint01 percent.
        /// </remarks>
        private float GetRatingValue(IUnitEntity entity, uint formulaId, float rating, float baseValue)
        {
            GameFormulaEntry formula = gameTableManager.GameFormula.GetEntry(formulaId);
            if (formula == null || entity.Level == 0u)
                return baseValue;

            float perRating = formula.Datafloat0 / entity.Level * formula.Datafloat01;
            float softCap = perRating != 0f ? (formula.Datafloat02 - formula.Dataint0) / perRating : float.MaxValue;

            float percent;
            if (rating < softCap)
                percent = perRating * rating + formula.Dataint0;
            else
            {
                float excess = rating - softCap;
                percent = MathF.Cos(excess / (excess + formula.Datafloat03) * MathF.PI * 0.5f) * excess * perRating + formula.Datafloat02;
            }

            return MathF.Min(baseValue + percent * 0.01f, formula.Dataint01 * 0.01f);
        }
    }
}
