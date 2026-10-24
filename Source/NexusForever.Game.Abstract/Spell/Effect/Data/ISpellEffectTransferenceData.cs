using NexusForever.Game.Static.Entity;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// DataBits02 and DataBits03 are the multiplier and flat amount of the magnitude, see IDamageCalculator.CalculateEffectMagnitude.
    /// </remarks>
    public interface ISpellEffectTransferenceData : ISpellEffectData
    {
        /// <summary>
        /// Vital drained from the target, Health (1) or ShieldCapacity (3).
        /// </summary>
        Vital DrainVital { get; }

        /// <summary>
        /// Vital the caster gains, always Health.
        /// </summary>
        Vital GainVital { get; }

        /// <summary>
        /// Fraction of the drained amount the caster gains.
        /// </summary>
        float GainFraction { get; }
    }
}
