namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectPathMissionIncrementData : ISpellEffectData
    {
        uint PathMissionId { get; }
        uint Count { get; }
    }
}
