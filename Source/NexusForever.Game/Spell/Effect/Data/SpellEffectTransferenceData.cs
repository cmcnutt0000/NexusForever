using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Entity;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectTransferenceData : ISpellEffectTransferenceData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public Vital DrainVital { get; private set; }
        public Vital GainVital { get; private set; }
        public float GainFraction { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry        = entry;
            DrainVital   = (Vital)entry.DataBits00;
            GainVital    = (Vital)entry.DataBits01;
            GainFraction = BitConverter.UInt32BitsToSingle(entry.DataBits05);
        }
    }
}
