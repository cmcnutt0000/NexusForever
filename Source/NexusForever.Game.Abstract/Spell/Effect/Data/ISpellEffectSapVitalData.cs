using NexusForever.Game.Static.Entity;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// The level up spell (410) restores 100% of health; DataBits01 is inferred to be a fraction of the maximum from explosion effects (0.5 and 1.5).
    /// </remarks>
    public interface ISpellEffectSapVitalData : ISpellEffectData
    {
        /// <summary>
        /// Vital that is changed.
        /// </summary>
        Vital Vital { get; }

        /// <summary>
        /// Fraction of the vital maximum, DataBits01 is a fraction and DataBits02 a percentage.
        /// </summary>
        float Fraction { get; }

        /// <summary>
        /// Restore the vital instead of removing it, DataBits03 is 1 to restore and 2 to remove.
        /// </summary>
        bool Restore { get; }
    }
}
