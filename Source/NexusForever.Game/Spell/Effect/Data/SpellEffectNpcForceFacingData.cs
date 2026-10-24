using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectNpcForceFacingData : ISpellEffectNpcForceFacingData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public float Angle { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry = entry;
            Angle = BitConverter.UInt32BitsToSingle(entry.DataBits00);
        }
    }
}
