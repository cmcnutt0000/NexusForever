using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectRestedXpDecorBonusData : ISpellEffectRestedXpDecorBonusData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public float RestXpBonus { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry       = entry;
            // 0.2, 0.4 and 0.6 on the small, medium and large housing decor auras
            RestXpBonus = BitConverter.UInt32BitsToSingle(entry.DataBits00);
        }
    }
}
