using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectUnlockInlaidAugmentData : ISpellEffectUnlockInlaidAugmentData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint EldanAugmentationId { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry               = entry;
            // for 221 of the 281 effects the augment spell has the name of the effect's spell, 33 Engineer AMPs all name 197
            EldanAugmentationId = entry.DataBits00;
        }
    }
}
