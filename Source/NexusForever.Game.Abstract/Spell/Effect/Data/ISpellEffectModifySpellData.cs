using NexusForever.GameTable.Model;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// The spells are picked by base spell or, without one, by spell group, see 0x1405DA6B0 in the client.
    /// </remarks>
    public interface ISpellEffectModifySpellData : ISpellEffectData
    {
        /// <summary>
        /// Base spell whose tiers are changed, 0 to use the spell group.
        /// </summary>
        uint Spell4BaseId { get; }

        /// <summary>
        /// Spell group whose spells are changed when there is no base spell.
        /// </summary>
        uint SpellGroupId { get; }

        /// <summary>
        /// Spell4Modification rows of the effect.
        /// </summary>
        IReadOnlyList<Spell4ModificationEntry> Modifications { get; }
    }
}
