using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectSpellEffectImmunityData : ISpellEffectData
    {
        /// <summary>
        /// Effect type the bearer is immune to.
        /// </summary>
        SpellEffectType EffectType { get; }
    }
}
