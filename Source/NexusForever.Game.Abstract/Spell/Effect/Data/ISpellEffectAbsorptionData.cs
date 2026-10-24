using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// Used by Absorption and HealingAbsorption, the hit fraction and damage types only apply to Absorption.
    /// </remarks>
    public interface ISpellEffectAbsorptionData : ISpellEffectData
    {
        /// <summary>
        /// Multiplier of the amount, 0 is treated as 1.
        /// </summary>
        float Multiplier { get; }

        /// <summary>
        /// Flat amount, the magnitude of the effect parameters is added to it.
        /// </summary>
        float Amount { get; }

        /// <summary>
        /// Fraction of each hit that can be absorbed, DataBits02 is a percentage and 0 is all of it.
        /// </summary>
        float HitFraction { get; }

        /// <summary>
        /// Mask of the absorbed damage types, 1 << (DamageType - 1); 7 (all), 1 (physical) and 6 (tech and magic) are used.
        /// </summary>
        uint DamageTypeMask { get; }
    }
}
