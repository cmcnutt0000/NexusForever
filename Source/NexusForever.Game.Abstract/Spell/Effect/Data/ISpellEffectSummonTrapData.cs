namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectSummonTrapData : ISpellEffectData
    {
        uint CreatureId { get; }
        uint SpellId { get; }
        TimeSpan ArmTime { get; }
        float Radius { get; }
        uint PrerequisiteId { get; }
        bool EnemiesOnly { get; }
    }
}
