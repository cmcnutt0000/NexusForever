using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectVendorPriceModifierData : ISpellEffectVendorPriceModifierData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public float BuyPriceMultiplier { get; private set; }
        public float SellPriceMultiplier { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry               = entry;
            // "Sell items for 5% more, buy items for 5% less" is 0.95 and 1.05, the CBT2 test spell has no sell multiplier
            BuyPriceMultiplier  = BitConverter.UInt32BitsToSingle(entry.DataBits00);
            SellPriceMultiplier = BitConverter.UInt32BitsToSingle(entry.DataBits01);
        }
    }
}
