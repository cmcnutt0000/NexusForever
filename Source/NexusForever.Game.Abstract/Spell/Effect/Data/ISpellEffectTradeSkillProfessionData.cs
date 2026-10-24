using NexusForever.Game.Static.Crafting;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectTradeSkillProfessionData : ISpellEffectData
    {
        TradeskillType TradeskillId { get; }
    }
}
