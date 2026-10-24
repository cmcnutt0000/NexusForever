using NexusForever.GameTable.Model;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectSummonCreatureData : ISpellEffectData
    {
        uint CreatureId { get; }
        uint Count { get; }
        uint MinDistance { get; }
        uint MaxDistance { get; }
        int Angle { get; }
        TelegraphDamageEntry TelegraphDamage { get; }
        uint SpellId { get; }
    }
}
