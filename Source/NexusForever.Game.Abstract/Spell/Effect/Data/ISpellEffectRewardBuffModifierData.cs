namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectRewardBuffModifierData : ISpellEffectData
    {
        uint ContentType { get; }
        float Unknown01 { get; }
        float Unknown02 { get; }
    }
}
