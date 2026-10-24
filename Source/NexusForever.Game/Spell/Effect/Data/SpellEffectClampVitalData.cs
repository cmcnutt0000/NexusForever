using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectClampVitalData : ISpellEffectClampVitalData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public bool Ceiling { get; private set; }
        public float Fraction { get; private set; }
        public bool PerStack { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry    = entry;
            Ceiling  = entry.DataBits00 == 1u;
            Fraction = BitConverter.UInt32BitsToSingle(entry.DataBits02);
            PerStack = entry.DataBits04 == 1u;
        }
    }
}
