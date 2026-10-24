namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectQuestAdvanceObjectiveData : ISpellEffectData
    {
        uint QuestId { get; }
        byte ObjectiveIndex { get; }
        uint Count { get; }
    }
}
