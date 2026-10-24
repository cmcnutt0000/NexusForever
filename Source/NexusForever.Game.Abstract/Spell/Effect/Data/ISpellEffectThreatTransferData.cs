using NexusForever.Game.Static.Combat;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectThreatTransferData : ISpellEffectData
    {
        /// <summary>
        /// How the threat moves.
        /// </summary>
        ThreatTransferType Type { get; }

        /// <summary>
        /// Fraction of the threat that moves, DataBits01 or the percentage in DataBits03 (Subtlety has 0 and 30).
        /// </summary>
        float Fraction { get; }
    }
}
