namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectActionBarSetData : ISpellEffectData
    {
        /// <summary>
        /// ActionBarShortcutSet shown to the target.
        /// </summary>
        uint ShortcutSetId { get; }

        uint Unknown01 { get; }
        uint Unknown02 { get; }
    }
}
