using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Crafting;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectTradeSkillProfessionData : ISpellEffectTradeSkillProfessionData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public TradeskillType TradeskillId { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry        = entry;
            // Fishing, Farmer and Cooking on the three "Learn ... Hobby" spells
            TradeskillId = (TradeskillType)entry.DataBits00;
        }
    }
}
