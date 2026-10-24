using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Entity;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectRewardPropertyModifierData : ISpellEffectRewardPropertyModifierData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public RewardPropertyType Property { get; private set; }
        public uint Data { get; private set; }
        public float Value { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry    = entry;
            Property = (RewardPropertyType)entry.DataBits00;
            // CurrencyType for currencies, reward track type for reward tracks, as in RewardPropertyPremiumModifier
            Data     = entry.DataBits01;

            // when dataBits03 is set tooltips show it rather than dataBits02 ("Flask of Fame"; "Flask of Minor Experience" is 15% with 1 in dataBits02)
            float value = BitConverter.UInt32BitsToSingle(entry.DataBits03);
            Value    = value != 0f ? value : BitConverter.UInt32BitsToSingle(entry.DataBits02);
        }
    }
}
