using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Combat;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectThreatTransferData : ISpellEffectThreatTransferData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public ThreatTransferType Type { get; private set; }
        public float Fraction { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry = entry;
            Type  = (ThreatTransferType)entry.DataBits00;

            float fraction = BitConverter.UInt32BitsToSingle(entry.DataBits01);
            Fraction = fraction > 0f ? fraction : entry.DataBits03 / 100f;
        }
    }
}
