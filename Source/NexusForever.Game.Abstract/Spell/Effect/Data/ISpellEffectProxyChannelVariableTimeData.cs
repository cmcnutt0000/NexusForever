namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// Used for "Progressive Telegraph" creature attacks, every row with n spells has n - 1 gaps.
    /// </remarks>
    public interface ISpellEffectProxyChannelVariableTimeData : ISpellEffectData
    {
        /// <summary>
        /// Spells cast one after another, with the milliseconds after the first.
        /// </summary>
        IReadOnlyList<(uint Spell4Id, uint Delay)> Spells { get; }
    }
}
