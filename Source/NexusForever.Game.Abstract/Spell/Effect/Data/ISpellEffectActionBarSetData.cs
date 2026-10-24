using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectActionBarSetData : ISpellEffectData
    {
        /// <summary>
        /// ActionBarShortcutSet shown to the target.
        /// </summary>
        uint ShortcutSetId { get; }

        uint Unknown01 { get; }

        /// <summary>
        /// Bar the set is shown on.
        /// </summary>
        ShortcutSet Bar { get; }
    }
}
