namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// Up to 5 pairs of a spell and its weight, eg: Esper Premonition (1, 1, 1, 1) and weights of 20, 50 or 33 elsewhere.
    /// </remarks>
    public interface ISpellEffectProxyRandomExclusiveData : ISpellEffectData
    {
        /// <summary>
        /// Spells one of which is cast, with their weights.
        /// </summary>
        IReadOnlyList<(uint Spell4Id, uint Weight)> Spells { get; }
    }
}
