namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectGrantXpData : ISpellEffectData
    {
        uint Xp { get; }
        uint MaxLevel { get; }
        uint MinLevel { get; }
    }
}
