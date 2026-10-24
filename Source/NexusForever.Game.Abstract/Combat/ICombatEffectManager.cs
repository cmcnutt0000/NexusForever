using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Combat;
using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Abstract.Combat
{
    /// <summary>
    /// Combat state spell effects put on a unit while they are active: absorbs, damage and healing modifiers, forced outcomes, immunities, unit states, health clamps and death prevention.
    /// </summary>
    public interface ICombatEffectManager
    {
        void Initialise(IUnitEntity entity);

        /// <summary>
        /// Add the <see cref="ISpellEffectData"/> of an active effect.
        /// </summary>
        void AddEffect(ISpellTargetEffectInfo info, ISpellEffectData data);

        /// <summary>
        /// Add the <see cref="ISpellEffectAbsorptionData"/> of an active Absorption or HealingAbsorption effect with the amount it absorbs, <see cref="uint.MaxValue"/> absorbs without limit.
        /// </summary>
        void AddAbsorption(ISpellTargetEffectInfo info, ISpellEffectAbsorptionData data, uint amount);

        /// <summary>
        /// Remove the state of an effect that is no longer active.
        /// </summary>
        void RemoveEffect(ISpellTargetEffectInfo info);

        /// <summary>
        /// Return whether an effect of the supplied <see cref="SpellEffectType"/> is active.
        /// </summary>
        bool HasEffect(SpellEffectType type);

        /// <summary>
        /// Return the active effects of the supplied <see cref="SpellEffectType"/>.
        /// </summary>
        IEnumerable<ISpellTargetEffectInfo> GetEffects(SpellEffectType type);

        /// <summary>
        /// Return the <see cref="ISpellEffectData"/> of the active effects of the supplied <see cref="SpellEffectType"/>.
        /// </summary>
        IEnumerable<T> GetEffectData<T>(SpellEffectType type) where T : ISpellEffectData;

        /// <summary>
        /// Return the amount of damage of the supplied <see cref="DamageType"/> absorbed by Absorption effects, the absorbed amount is used up.
        /// </summary>
        uint AbsorbDamage(DamageType damageType, uint damage);

        /// <summary>
        /// Return the amount of healing absorbed by HealingAbsorption effects, the absorbed amount is used up.
        /// </summary>
        uint AbsorbHealing(uint healing);

        /// <summary>
        /// Return the multiplier of damage of the supplied <see cref="DamageType"/> dealt to the victim.
        /// </summary>
        float GetDamageDealtMultiplier(IUnitEntity victim, DamageType damageType);

        /// <summary>
        /// Return the multiplier of damage of the supplied <see cref="DamageType"/> taken from the attacker.
        /// </summary>
        float GetDamageTakenMultiplier(IUnitEntity attacker, DamageType damageType);

        /// <summary>
        /// Return the flat reduction of damage taken from the attacker.
        /// </summary>
        uint GetDamageTakenReduction(IUnitEntity attacker);

        /// <summary>
        /// Return the multiplier of healing dealt to the target.
        /// </summary>
        float GetHealingDealtMultiplier(IUnitEntity target);

        /// <summary>
        /// Return the multiplier of healing taken from the healer.
        /// </summary>
        float GetHealingTakenMultiplier(IUnitEntity healer);

        /// <summary>
        /// Return the <see cref="ForcedOutcome"/> of the next attack made by, or against, the unit, a limited outcome is used up.
        /// </summary>
        ForcedOutcome? ConsumeForcedOutcome(bool againstBearer);

        /// <summary>
        /// Return whether the supplied <see cref="UnitState"/> is set.
        /// </summary>
        bool HasUnitState(UnitState state);

        /// <summary>
        /// Return whether the unit can't take damage.
        /// </summary>
        bool IsInvulnerable { get; }

        /// <summary>
        /// Return whether the unit's shields are disabled by a ShieldOverload effect.
        /// </summary>
        bool IsShieldOverloaded { get; }

        /// <summary>
        /// Return whether the unit is immune to an effect of the supplied <see cref="SpellEffectType"/> from the <see cref="ISpell"/>.
        /// </summary>
        bool IsImmune(ISpell spell, SpellEffectType effectType);

        /// <summary>
        /// Return the minimum health ClampVital effects allow.
        /// </summary>
        uint GetMinimumHealth();

        /// <summary>
        /// Return the maximum current health ClampVital effects allow.
        /// </summary>
        uint GetMaximumHealth();

        /// <summary>
        /// Return whether a DelayDeath effect prevents the unit dying and the spell it casts instead.
        /// </summary>
        bool TryPreventDeath(out uint spell4Id);
    }
}
