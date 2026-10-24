using NexusForever.Game.Static.Entity;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectRewardPropertyModifierData : ISpellEffectData
    {
        RewardPropertyType Property { get; }
        uint Data { get; }
        float Value { get; }
    }
}
