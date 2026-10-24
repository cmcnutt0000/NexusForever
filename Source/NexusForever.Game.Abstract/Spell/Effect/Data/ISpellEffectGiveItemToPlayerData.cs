namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectGiveItemToPlayerData : ISpellEffectData
    {
        uint ItemId { get; }
        uint Count { get; }
    }
}
