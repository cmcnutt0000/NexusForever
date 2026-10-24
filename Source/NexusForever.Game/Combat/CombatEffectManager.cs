using NexusForever.Game.Abstract.Combat;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Info;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Combat;
using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Combat
{
    public class CombatEffectManager : ICombatEffectManager
    {
        private class CombatEffect
        {
            public ISpellTargetEffectInfo Info { get; init; }
            public ISpellEffectData Data { get; init; }
            public uint CasterGuid { get; init; }
            public uint CastingId { get; init; }

            /// <summary>
            /// Amount an absorb has left or attacks a forced outcome has left, <see cref="uint.MaxValue"/> is unlimited.
            /// </summary>
            public uint Remaining { get; set; }
        }

        /// <summary>
        /// Return whether the unit can't take damage.
        /// </summary>
        public bool IsInvulnerable => HasUnitState(UnitState.Invulnerable) || HasUnitState(UnitState.DamageImmune);

        /// <summary>
        /// Return whether the unit's shields are disabled by a ShieldOverload effect.
        /// </summary>
        public bool IsShieldOverloaded => HasEffect(SpellEffectType.ShieldOverload);

        private IUnitEntity owner;
        private readonly List<CombatEffect> effects = [];

        // next time each DelayDeath effect can trigger, keyed by Spell4Effects id
        private readonly Dictionary<uint, DateTime> deathPreventionCooldowns = [];

        #region Dependency Injection

        private readonly IPrerequisiteManager prerequisiteManager;

        public CombatEffectManager(
            IPrerequisiteManager prerequisiteManager)
        {
            this.prerequisiteManager = prerequisiteManager;
        }

        #endregion

        public void Initialise(IUnitEntity entity)
        {
            if (owner != null)
                throw new InvalidOperationException("CombatEffectManager has already been initialised!");

            owner = entity;
        }

        /// <summary>
        /// Add the <see cref="ISpellEffectData"/> of an active effect.
        /// </summary>
        public void AddEffect(ISpellTargetEffectInfo info, ISpellEffectData data)
        {
            uint remaining = 0u;
            if (data is ISpellEffectForcedActionData forcedAction)
                remaining = forcedAction.Count is > 0 and < 999 ? (uint)forcedAction.Count : uint.MaxValue;

            AddEffect(info, data, remaining);
        }

        /// <summary>
        /// Add the <see cref="ISpellEffectAbsorptionData"/> of an active Absorption or HealingAbsorption effect with the amount it absorbs, <see cref="uint.MaxValue"/> absorbs without limit.
        /// </summary>
        public void AddAbsorption(ISpellTargetEffectInfo info, ISpellEffectAbsorptionData data, uint amount)
        {
            AddEffect(info, data, amount);
        }

        private void AddEffect(ISpellTargetEffectInfo info, ISpellEffectData data, uint remaining)
        {
            // periodic effects are applied again each tick, the state is kept from the first
            if (effects.Any(e => e.Info == info))
                return;

            ISpell spell = info.Target.Collection.Spell;
            effects.Add(new CombatEffect
            {
                Info       = info,
                Data       = data,
                CasterGuid = spell.Caster.Guid,
                CastingId  = spell.CastingId,
                Remaining  = remaining
            });
        }

        /// <summary>
        /// Remove the state of an effect that is no longer active.
        /// </summary>
        public void RemoveEffect(ISpellTargetEffectInfo info)
        {
            effects.RemoveAll(e => e.Info == info);
        }

        /// <summary>
        /// Return whether an effect of the supplied <see cref="SpellEffectType"/> is active.
        /// </summary>
        public bool HasEffect(SpellEffectType type)
        {
            return effects.Any(e => e.Info.Entry.EffectType == type);
        }

        /// <summary>
        /// Return the active effects of the supplied <see cref="SpellEffectType"/>.
        /// </summary>
        public IEnumerable<ISpellTargetEffectInfo> GetEffects(SpellEffectType type)
        {
            // copied, finishing an effect removes it from the list
            return effects
                .Where(e => e.Info.Entry.EffectType == type)
                .Select(e => e.Info)
                .ToList();
        }

        /// <summary>
        /// Return the <see cref="ISpellEffectData"/> of the active effects of the supplied <see cref="SpellEffectType"/>.
        /// </summary>
        public IEnumerable<T> GetEffectData<T>(SpellEffectType type) where T : ISpellEffectData
        {
            return GetEffects<T>(type)
                .Select(e => e.Data)
                .ToList();
        }

        private IEnumerable<(CombatEffect Effect, T Data)> GetEffects<T>(SpellEffectType type) where T : ISpellEffectData
        {
            foreach (CombatEffect effect in effects)
                if (effect.Info.Entry.EffectType == type && effect.Data is T data)
                    yield return (effect, data);
        }

        /// <summary>
        /// Return the amount of damage of the supplied <see cref="DamageType"/> absorbed by Absorption effects, the absorbed amount is used up.
        /// </summary>
        public uint AbsorbDamage(DamageType damageType, uint damage)
        {
            uint absorbed = 0u;
            foreach ((CombatEffect effect, ISpellEffectAbsorptionData data) in GetEffects<ISpellEffectAbsorptionData>(SpellEffectType.Absorption))
            {
                if (absorbed >= damage)
                    break;

                if (effect.Remaining == 0u)
                    continue;

                if (data.DamageTypeMask != 0u
                    && damageType is DamageType.Physical or DamageType.Tech or DamageType.Magic
                    && (data.DamageTypeMask & (1u << (damageType - DamageType.Physical))) == 0u)
                    continue;

                uint amount = Math.Min((uint)MathF.Round((damage - absorbed) * data.HitFraction), effect.Remaining);
                if (effect.Remaining != uint.MaxValue)
                    effect.Remaining -= amount;

                absorbed += amount;
            }

            return absorbed;
        }

        /// <summary>
        /// Return the amount of healing absorbed by HealingAbsorption effects, the absorbed amount is used up.
        /// </summary>
        public uint AbsorbHealing(uint healing)
        {
            uint absorbed = 0u;
            foreach ((CombatEffect effect, _) in GetEffects<ISpellEffectAbsorptionData>(SpellEffectType.HealingAbsorption))
            {
                if (absorbed >= healing)
                    break;

                uint amount = Math.Min(healing - absorbed, effect.Remaining);
                if (effect.Remaining != uint.MaxValue)
                    effect.Remaining -= amount;

                absorbed += amount;
            }

            return absorbed;
        }

        private float GetPersonalMultiplier(PersonalModifierType type, IUnitEntity other)
        {
            float multiplier = 1f;
            foreach ((CombatEffect effect, ISpellEffectPersonalModifierData data) in GetEffects<ISpellEffectPersonalModifierData>(SpellEffectType.PersonalDmgHealMod))
                if (data.Type == type && (!data.CasterOnly || effect.CasterGuid == other.Guid))
                    multiplier *= data.Multiplier;

            return multiplier;
        }

        /// <summary>
        /// Return the multiplier of damage of the supplied <see cref="DamageType"/> dealt to the victim.
        /// </summary>
        public float GetDamageDealtMultiplier(IUnitEntity victim, DamageType damageType)
        {
            if (damageType is not (DamageType.Physical or DamageType.Tech or DamageType.Magic))
                return 1f;

            return GetPersonalMultiplier(PersonalModifierType.DamageDealtPhysical + (damageType - DamageType.Physical), victim);
        }

        /// <summary>
        /// Return the multiplier of damage of the supplied <see cref="DamageType"/> taken from the attacker.
        /// </summary>
        public float GetDamageTakenMultiplier(IUnitEntity attacker, DamageType damageType)
        {
            if (damageType is not (DamageType.Physical or DamageType.Tech or DamageType.Magic))
                return 1f;

            return GetPersonalMultiplier(PersonalModifierType.DamageTakenPhysical + (damageType - DamageType.Physical), attacker);
        }

        /// <summary>
        /// Return the flat reduction of damage taken from the attacker.
        /// </summary>
        public uint GetDamageTakenReduction(IUnitEntity attacker)
        {
            uint reduction = 0u;
            foreach ((CombatEffect effect, ISpellEffectPersonalModifierData data) in GetEffects<ISpellEffectPersonalModifierData>(SpellEffectType.PersonalDmgHealMod))
                if (data.Type == PersonalModifierType.DamageTakenFlat && (!data.CasterOnly || effect.CasterGuid == attacker.Guid))
                    reduction += data.FlatValue;

            return reduction;
        }

        /// <summary>
        /// Return the multiplier of healing dealt to the target.
        /// </summary>
        public float GetHealingDealtMultiplier(IUnitEntity target)
        {
            return GetPersonalMultiplier(PersonalModifierType.HealingDealt, target);
        }

        /// <summary>
        /// Return the multiplier of healing taken from the healer.
        /// </summary>
        public float GetHealingTakenMultiplier(IUnitEntity healer)
        {
            return GetPersonalMultiplier(PersonalModifierType.HealingTaken, healer);
        }

        /// <summary>
        /// Return the <see cref="ForcedOutcome"/> of the next attack made by, or against, the unit, a limited outcome is used up.
        /// </summary>
        public ForcedOutcome? ConsumeForcedOutcome(bool againstBearer)
        {
            foreach ((CombatEffect effect, ISpellEffectForcedActionData data) in GetEffects<ISpellEffectForcedActionData>(SpellEffectType.ForcedAction))
            {
                if (data.AgainstBearer != againstBearer || effect.Remaining == 0u)
                    continue;

                if (data.Chance < 1f && Random.Shared.NextSingle() >= data.Chance)
                    continue;

                if (effect.Remaining != uint.MaxValue)
                    effect.Remaining--;

                return data.Outcome;
            }

            return null;
        }

        /// <summary>
        /// Return whether the supplied <see cref="UnitState"/> is set.
        /// </summary>
        public bool HasUnitState(UnitState state)
        {
            return GetEffects<ISpellEffectUnitStateSetData>(SpellEffectType.UnitStateSet).Any(e => e.Data.State == state);
        }

        /// <summary>
        /// Return whether the unit is immune to an effect of the supplied <see cref="SpellEffectType"/> from the <see cref="ISpell"/>.
        /// </summary>
        public bool IsImmune(ISpell spell, SpellEffectType effectType)
        {
            foreach (CombatEffect effect in effects)
            {
                // a spell doesn't make its target immune to the rest of its own effects
                if (effect.CastingId == spell.CastingId)
                    continue;

                switch (effect.Data)
                {
                    case ISpellEffectSpellEffectImmunityData effectImmunity when effectImmunity.EffectType == effectType:
                        return true;
                    case ISpellEffectSpellImmunityData spellImmunity when IsImmune(spellImmunity, spell.Parameters.SpellInfo):
                        return true;
                }
            }

            return false;
        }

        private static bool IsImmune(ISpellEffectSpellImmunityData data, ISpellInfo spellInfo)
        {
            return data.Type switch
            {
                SpellImmunityType.Spell      => spellInfo.Entry.Id == data.Id,
                SpellImmunityType.BaseSpell  => spellInfo.BaseInfo.Entry.Id == data.Id,
                SpellImmunityType.SpellGroup => spellInfo.SpellGroups.Contains(data.Id),
                _                            => false
            };
        }

        /// <summary>
        /// Return the units that receive part of the threat the owner generates and the fraction each receives.
        /// </summary>
        public IEnumerable<(uint ReceiverGuid, float Fraction)> GetThreatTransfers()
        {
            foreach ((CombatEffect effect, ISpellEffectThreatTransferData data) in GetEffects<ISpellEffectThreatTransferData>(SpellEffectType.ThreatTransfer))
            {
                // BearerToCaster is kept by the bearer and goes to the caster, CasterToBearer is kept by the caster and goes to the bearer
                uint receiverGuid = data.Type == ThreatTransferType.CasterToBearer ? effect.Info.Target.Guid : effect.CasterGuid;
                if (receiverGuid != owner.Guid)
                    yield return (receiverGuid, data.Fraction);
            }
        }

        /// <summary>
        /// Return the live units, the owner included, that share health with the owner through a SharedHealthPool effect.
        /// </summary>
        public IReadOnlyList<IUnitEntity> GetSharedHealthPool()
        {
            CombatEffect pool = effects.FirstOrDefault(e => e.Info.Entry.EffectType == SpellEffectType.SharedHealthPool);
            if (pool == null)
                return [];

            // every target of the same cast with the effect still active
            return pool.Info.Target.Collection
                .Where(t => !t.IsFinalised && t.GetEffectsByType(SpellEffectType.SharedHealthPool).Any(e => !e.IsFinalised && !e.IsPending))
                .Select(t => t.GetTarget())
                .Where(u => u != null && u.IsAlive)
                .ToList();
        }

        /// <summary>
        /// Return the minimum health ClampVital effects allow.
        /// </summary>
        public uint GetMinimumHealth()
        {
            float fraction = 0f;
            foreach ((_, ISpellEffectClampVitalData data) in GetEffects<ISpellEffectClampVitalData>(SpellEffectType.ClampVital))
                if (!data.Ceiling)
                    fraction = MathF.Max(fraction, data.Fraction);

            if (fraction <= 0f)
                return 0u;

            return Math.Max(1u, (uint)MathF.Ceiling(owner.MaxHealth * MathF.Min(fraction, 1f)));
        }

        /// <summary>
        /// Return the maximum current health ClampVital effects allow.
        /// </summary>
        public uint GetMaximumHealth()
        {
            float reduction = 0f;
            foreach (IGrouping<uint, (CombatEffect Effect, ISpellEffectClampVitalData Data)> group in GetEffects<ISpellEffectClampVitalData>(SpellEffectType.ClampVital)
                .Where(e => e.Data.Ceiling)
                .GroupBy(e => e.Effect.Info.Entry.Id))
            {
                // each stack is its own effect, only a stacking clamp lowers the ceiling more than once
                ISpellEffectClampVitalData data = group.First().Data;
                reduction += data.PerStack ? data.Fraction * group.Count() : data.Fraction;
            }

            if (reduction <= 0f)
                return owner.MaxHealth;

            return Math.Max(1u, (uint)(owner.MaxHealth * MathF.Max(0f, 1f - reduction)));
        }

        /// <summary>
        /// Return whether a DelayDeath effect prevents the unit dying and the spell it casts instead.
        /// </summary>
        public bool TryPreventDeath(out uint spell4Id)
        {
            foreach ((CombatEffect effect, ISpellEffectDelayDeathData data) in GetEffects<ISpellEffectDelayDeathData>(SpellEffectType.DelayDeath))
            {
                if (data.PrerequisiteId != 0u && !prerequisiteManager.Meets(owner, data.PrerequisiteId))
                    continue;

                if (deathPreventionCooldowns.TryGetValue(effect.Info.Entry.Id, out DateTime next) && next > DateTime.UtcNow)
                    continue;

                if (data.Cooldown != 0u)
                    deathPreventionCooldowns[effect.Info.Entry.Id] = DateTime.UtcNow.AddMilliseconds(data.Cooldown);

                spell4Id = data.Spell4Id;
                return true;
            }

            spell4Id = 0u;
            return false;
        }
    }
}
