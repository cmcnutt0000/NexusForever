using NexusForever.Game.Static.Spell;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// The effects are picked by Spell4Effects id, or with an effect type by spell group or all the bearer's spells, see 0x1405D91F0 in the client.
    /// </remarks>
    public interface ISpellEffectModifySpellEffectData : ISpellEffectData
    {
        /// <summary>
        /// Spell4Effects row that is changed by every modification, 0 to pick effects by type.
        /// </summary>
        uint Spell4EffectsId { get; }

        /// <summary>
        /// Id of a list the client looks up, what it lists isn't known (10 rows).
        /// </summary>
        uint ListId { get; }

        /// <summary>
        /// Change the effects of every spell of the bearer.
        /// </summary>
        bool AllSpells { get; }

        /// <summary>
        /// Spell group whose spells' effects are changed.
        /// </summary>
        uint SpellGroupId { get; }

        /// <summary>
        /// Effect type that has to be set for the spell group and all spells to be used, the modifications pick the effects by their own type.
        /// </summary>
        SpellEffectType EffectType { get; }

        /// <summary>
        /// Spell4EffectModification rows of the effect.
        /// </summary>
        IReadOnlyList<Spell4EffectModificationEntry> Modifications { get; }
    }
}
