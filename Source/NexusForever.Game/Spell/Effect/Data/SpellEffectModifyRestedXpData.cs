using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectModifyRestedXpData : ISpellEffectModifyRestedXpData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public float Levels { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry  = entry;
            // multiple of the current level's experience, 1.5 "restores bonus rest XP to maximum" and 5 "fills your Rest XP bar"
            Levels = BitConverter.UInt32BitsToSingle(entry.DataBits00);
        }
    }
}
