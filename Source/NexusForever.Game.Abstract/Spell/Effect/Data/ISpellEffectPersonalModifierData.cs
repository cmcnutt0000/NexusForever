using NexusForever.Game.Static.Combat;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectPersonalModifierData : ISpellEffectData
    {
        /// <summary>
        /// What is modified.
        /// </summary>
        PersonalModifierType Type { get; }

        /// <summary>
        /// Only modify damage and healing between the bearer and the caster of the effect, DataBits01 is 1 or 2.
        /// </summary>
        bool CasterOnly { get; }

        /// <summary>
        /// Multiplier of the damage or healing.
        /// </summary>
        float Multiplier { get; }

        /// <summary>
        /// Flat damage reduction of DamageTakenFlat, the tooltip shows it as an integer.
        /// </summary>
        uint FlatValue { get; }
    }
}
