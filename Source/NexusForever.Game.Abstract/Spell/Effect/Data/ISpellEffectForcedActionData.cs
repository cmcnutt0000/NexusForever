using NexusForever.Game.Static.Combat;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// DataBits03 is a float, 43 effects store an integer 1 instead which is read as 100%.
    /// </remarks>
    public interface ISpellEffectForcedActionData : ISpellEffectData
    {
        /// <summary>
        /// Outcome forced on the attack.
        /// </summary>
        ForcedOutcome Outcome { get; }

        /// <summary>
        /// Number of attacks, 999 and -1 are used for unlimited.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Force the outcome of attacks against the bearer instead of attacks made by the bearer.
        /// </summary>
        bool AgainstBearer { get; }

        /// <summary>
        /// Chance of the outcome.
        /// </summary>
        float Chance { get; }
    }
}
