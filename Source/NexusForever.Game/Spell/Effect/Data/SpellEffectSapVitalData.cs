using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Entity;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectSapVitalData : ISpellEffectSapVitalData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public Vital Vital { get; private set; }
        public float Fraction { get; private set; }
        public bool Restore { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry    = entry;
            Vital    = (Vital)entry.DataBits00;
            Fraction = BitConverter.UInt32BitsToSingle(entry.DataBits01) + BitConverter.UInt32BitsToSingle(entry.DataBits02) / 100f;
            Restore  = entry.DataBits03 == 1u;
        }
    }
}
