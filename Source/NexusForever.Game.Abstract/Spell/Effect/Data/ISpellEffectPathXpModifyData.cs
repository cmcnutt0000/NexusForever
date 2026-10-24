namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectPathXpModifyData : ISpellEffectData
    {
        int Amount { get; }
        bool IsLevel { get; }
    }
}
