using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectSupportStuckData : ISpellEffectSupportStuckData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public bool IsFree { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry  = entry;
            // the bits of 1.0 in "Support Stuck - Free Suicide", 0 in "Support Stuck - Durability Loss Suicide" (inferred from the names)
            IsFree = BitConverter.UInt32BitsToSingle(entry.DataBits00) != 0f;
        }
    }
}
