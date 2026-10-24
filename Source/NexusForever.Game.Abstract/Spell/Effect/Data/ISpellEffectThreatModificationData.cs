using NexusForever.Game.Static.Combat;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectThreatModificationData : ISpellEffectData
    {
        /// <summary>
        /// How threat is changed.
        /// </summary>
        ThreatModificationType Type { get; }

        /// <summary>
        /// Multiplier of Multiply or percentage of ReducePercent.
        /// </summary>
        float Multiplier { get; }

        /// <summary>
        /// Threat added by Add.
        /// </summary>
        uint Amount { get; }
    }
}
