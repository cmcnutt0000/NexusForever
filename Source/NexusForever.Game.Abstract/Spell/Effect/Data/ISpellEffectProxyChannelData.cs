namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// Every spell using it is channeled, the spells match the executions, eg: Distort Reality channels 4000ms with a 1000ms pulse and has 4 spells, Triple Beam channels 6000ms and casts on every other one.
    /// </remarks>
    public interface ISpellEffectProxyChannelData : ISpellEffectData
    {
        /// <summary>
        /// Spell cast on each execution of the channel, 0 casts nothing.
        /// </summary>
        IReadOnlyList<uint> Spells { get; }
    }
}
