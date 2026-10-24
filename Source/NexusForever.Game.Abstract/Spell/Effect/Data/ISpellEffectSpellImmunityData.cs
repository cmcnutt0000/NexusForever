using NexusForever.Game.Static.Combat;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectSpellImmunityData : ISpellEffectData
    {
        /// <summary>
        /// What the id refers to.
        /// </summary>
        SpellImmunityType Type { get; }

        /// <summary>
        /// Id of the spell, base spell or spell group.
        /// </summary>
        uint Id { get; }
    }
}
