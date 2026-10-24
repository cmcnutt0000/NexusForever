using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// Warrior Expulsion: "cleanse $(eff=119564.data00rawInt) debuff".
    /// </remarks>
    public interface ISpellEffectSpellDispelData : ISpellEffectData
    {
        /// <summary>
        /// Number of spells removed, 50 and -1 are used for all of them.
        /// </summary>
        uint Count { get; }

        /// <summary>
        /// Class of the spells removed, BuffDispellable (36) or DebuffDispellable (38).
        /// </summary>
        SpellClass SpellClass { get; }
    }
}
