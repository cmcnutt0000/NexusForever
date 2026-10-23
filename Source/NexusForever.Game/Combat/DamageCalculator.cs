using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Combat;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Target;
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
            damageDescription.DamageType   = info.Entry.DamageType;
            damageDescription.CombatResult = CombatResult.Hit;

            var castData = new CombatLogCastData
            {
                CasterId     = executionContext.Spell.Caster.Guid,
                TargetId     = victim.Guid,
                SpellId      = executionContext.Spell.Parameters.SpellInfo.Entry.Id, // TODO: This was updated in order to use ISpell, check if correct
                CombatResult = damageDescription.CombatResult
            };

            if (CalculateDeflect(executionContext.Spell.Caster, victim))
            {
                damageDescription.CombatResult = CombatResult.Avoid;
                executionContext.AddCombatLog(new CombatLogDeflect
                    {
                        BMultiHit = false,
                        CastData  = castData
                    });
                return;
            }

            uint damage = CalculateBaseDamage(executionContext.Spell.Caster, victim, info.Entry);
            damageDescription.RawDamage       = damage;
            damageDescription.RawScaledDamage = damage;

            damage = CalculateBaseDamageVariance(damage);

            damage = GetDamageAfterArmorMitigation(victim, info.Entry.DamageType, damage);

            // TODO: Add in other attacking modifiers like Armor Pierce, Strikethrough, Multi-Hit, etc.

            if (CalculateCrit(ref damage, executionContext.Spell.Caster, victim))
                damageDescription.CombatResult = CombatResult.Critical;

            uint preGlanceDamage = damage;
            if (CalculateGlance(ref damage, executionContext.Spell.Caster, victim))
            {
                uint glanceDamage = preGlanceDamage - damage;
                // TODO: Add CombatLog
            }

            uint shieldedAmount = CalculateShieldAmount(damage, victim);
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
        /// Get base damage value for the given <see cref="IUnitEntity"/> with the provided parameter data from the <see cref="Spell4EffectsEntry"/>.
        /// </summary>
        private uint CalculateBaseDamage(IUnitEntity caster, IUnitEntity target, Spell4EffectsEntry entry)
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

            return (uint)(propertyMultiplier * baseDamage);
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
            GameFormulaEntry armorFormulaEntry = gameTableManager.GameFormula.GetEntry(1234);
            float maximumArmorMitigation = (float)(armorFormulaEntry.Dataint01 * 0.01);
            float mitigationPct = (armorFormulaEntry.Datafloat0 / victim.Level * armorFormulaEntry.Datafloat01) * victim.GetPropertyValue(Property.Armor) / 100;

            if (damageType == DamageType.Physical)
                mitigationPct += victim.GetPropertyValue(Property.DamageMitigationPctOffsetPhysical);
            else if (damageType == DamageType.Tech)
                mitigationPct += victim.GetPropertyValue(Property.DamageMitigationPctOffsetTech);
            else if (damageType == DamageType.Magic)
                mitigationPct += victim.GetPropertyValue(Property.DamageMitigationPctOffsetMagic);

            if (mitigationPct > 0f)
                damage = (uint)Math.Round(damage * (1f - Math.Clamp(mitigationPct, 0f, maximumArmorMitigation)));

            return damage;
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
        private bool CalculateCrit(ref uint damage, IUnitEntity attacker, IUnitEntity victim)
        {
            // TODO: Add in Crit Deflect and Critical Mitigation calculations

            float critRate = GetRatingPercentMod(Property.RatingCritChanceIncrease, attacker);
            if (critRate <= 0f)
                return false;

            bool crit = IsSuccessfulChance(critRate);
            if (crit)
                damage = (uint)Math.Round(damage * GetRatingPercentMod(Property.RatingCritSeverityIncrease, attacker));

            return crit;
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

        private float GetRatingPercentMod(Property property, IUnitEntity entity)
        {
            GameFormulaEntry gameFormula = null;

            switch (property)
            {
                case Property.Armor:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1234);
                    break;
                case Property.RatingArmorPierce:
                case Property.IgnoreArmorBase:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1269);
                    break;
                case Property.RatingAvoidReduce: // Strikethrough
                case Property.BaseAvoidReduceChance:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1230);
                    break;
                case Property.RatingAvoidIncrease: // Deflect
                case Property.BaseAvoidChance:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1235);
                    break;
                case Property.RatingCriticalMitigation:
                case Property.BaseCriticalMitigation:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1236);
                    break;
                case Property.RatingCritChanceDecrease: // Deflect Crit Chance
                case Property.BaseAvoidCritChance:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1236);
                    break;
                case Property.RatingCritChanceIncrease:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1231);
                    break;
                case Property.RatingCritSeverityIncrease:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1232);
                    break;
                case Property.CCDurationModifier:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1274);
                    break;
                case Property.RatingDamageReflectAmount:
                case Property.BaseDamageReflectAmount:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1272);
                    break;
                case Property.RatingDamageReflectChance:
                case Property.BaseDamageReflectChance:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1241);
                    break;
                case Property.RatingFocusRecovery:
                case Property.BaseFocusRecoveryInCombat:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1237);
                    break;
                case Property.RatingGlanceAmount:
                case Property.BaseGlanceAmount:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1271);
                    break;
                case Property.RatingGlanceChance:
                case Property.BaseGlanceChance:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1245);
                    break;
                case Property.RatingIntensity:
                case Property.BaseIntensity:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1243);
                    break;
                case Property.RatingLifesteal:
                case Property.BaseLifesteal:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1233);
                    break;
                case Property.RatingMultiHitAmount:
                case Property.BaseMultiHitAmount:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1270);
                    break;
                case Property.RatingMultiHitChance:
                case Property.BaseMultiHitChance:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1240);
                    break;
                case Property.RatingVigor:
                case Property.BaseVigor:
                    gameFormula = gameTableManager.GameFormula.GetEntry(1244);
                    break;
                default:
                    log.LogWarning($"Unhandled Property in calculating Percentage from Rating: {property}");
                    break;
            }

            // Return a decimal representing the % applied by this rating.
            float ratingMod = ((gameFormula.Datafloat0 / entity.Level) * gameFormula.Datafloat01) * entity.GetPropertyValue(property);
            return Math.Min((GetBasePercentMod(property, entity) + ratingMod) * 100f, gameFormula.Dataint01) / 100f;
        }

        private float GetBasePercentMod(Property property, IUnitEntity entity)
        {
            float baseValue = 0f;
            switch (property)
            {
                case Property.RatingArmorPierce:
                case Property.IgnoreArmorBase:
                    baseValue = entity.GetPropertyValue(Property.IgnoreArmorBase);
                    break;
                case Property.RatingAvoidReduce: // Strikethrough
                case Property.BaseAvoidReduceChance:
                    baseValue = entity.GetPropertyValue(Property.BaseAvoidReduceChance);
                    break;
                case Property.RatingAvoidIncrease: // Deflect
                case Property.BaseAvoidChance:
                    baseValue = entity.GetPropertyValue(Property.BaseAvoidChance);
                    break;
                case Property.RatingCriticalMitigation:
                case Property.BaseCriticalMitigation:
                    baseValue = entity.GetPropertyValue(Property.BaseCriticalMitigation);
                    break;
                case Property.RatingCritChanceDecrease: // Deflect Crit Chance
                case Property.BaseAvoidCritChance:
                    baseValue = entity.GetPropertyValue(Property.BaseAvoidCritChance);
                    break;
                case Property.RatingCritChanceIncrease:
                case Property.BaseCritChance:
                    baseValue = entity.GetPropertyValue(Property.BaseCritChance);
                    break;
                case Property.RatingCritSeverityIncrease:
                    // TODO: Confirm Property below
                    // baseValue = entity.GetPropertyValue(Property.CriticalHitSeverityMultiplier);
                    break;
                case Property.RatingDamageReflectAmount:
                case Property.BaseDamageReflectAmount:
                    baseValue = entity.GetPropertyValue(Property.BaseDamageReflectAmount);
                    break;
                case Property.RatingDamageReflectChance:
                case Property.BaseDamageReflectChance:
                    baseValue = entity.GetPropertyValue(Property.BaseDamageReflectChance);
                    break;
                case Property.RatingFocusRecovery:
                case Property.BaseFocusRecoveryInCombat:
                    baseValue = entity.GetPropertyValue(Property.BaseFocusRecoveryInCombat);
                    break;
                case Property.RatingGlanceAmount:
                case Property.BaseGlanceAmount:
                    baseValue = entity.GetPropertyValue(Property.BaseGlanceAmount);
                    break;
                case Property.RatingGlanceChance:
                case Property.BaseGlanceChance:
                    baseValue = entity.GetPropertyValue(Property.BaseGlanceChance);
                    break;
                case Property.RatingIntensity:
                case Property.BaseIntensity:
                    baseValue = entity.GetPropertyValue(Property.BaseIntensity);
                    break;
                case Property.RatingLifesteal:
                case Property.BaseLifesteal:
                    baseValue = entity.GetPropertyValue(Property.BaseLifesteal);
                    break;
                case Property.RatingMultiHitAmount:
                case Property.BaseMultiHitAmount:
                    baseValue = entity.GetPropertyValue(Property.BaseMultiHitAmount);
                    break;
                case Property.RatingMultiHitChance:
                case Property.BaseMultiHitChance:
                    baseValue = entity.GetPropertyValue(Property.BaseMultiHitChance);
                    break;
                case Property.RatingVigor:
                case Property.BaseVigor:
                    baseValue = entity.GetPropertyValue(Property.BaseVigor);
                    break;
                default:
                    log.LogWarning($"Unhandled Property in calculating Percentage from Base: {property}");
                    break;
            }

            return baseValue;
        }
    }
}
