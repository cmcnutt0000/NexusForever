namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectAchievementAdvanceData : ISpellEffectData
    {
        uint AchievementId { get; }
        uint Count { get; }
    }
}
