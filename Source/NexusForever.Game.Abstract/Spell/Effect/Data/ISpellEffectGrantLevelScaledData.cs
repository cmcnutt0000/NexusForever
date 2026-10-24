namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectGrantLevelScaledData : ISpellEffectData
    {
        float Multiplier { get; }
        uint MaxLevel { get; }
        uint MinLevel { get; }
    }
}
