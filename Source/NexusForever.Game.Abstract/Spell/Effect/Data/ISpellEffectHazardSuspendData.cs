namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectHazardSuspendData : ISpellEffectData
    {
        uint HazardId { get; }
        uint Unknown01 { get; }
    }
}
