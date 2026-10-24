namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// Eg: "Adds the Thayd Transmat Terminal to your recall list" and the riding licenses.
    /// </remarks>
    public interface ISpellEffectAddSpellData : ISpellEffectData
    {
        /// <summary>
        /// Spell added to the target's spells.
        /// </summary>
        uint Spell4Id { get; }
    }
}
