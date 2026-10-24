using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectDisembarkData : ISpellEffectData
    {
        SpellEffectDisembarkFlags Flags { get; }
    }
}
