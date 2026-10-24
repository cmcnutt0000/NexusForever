using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectRavelSignalData : ISpellEffectData
    {
        SpellEffectRavelSignalScope Scope { get; }
        uint SignalId { get; }
        int Value { get; }
        uint WorldSocketId { get; }
        float Radius { get; }
    }
}
