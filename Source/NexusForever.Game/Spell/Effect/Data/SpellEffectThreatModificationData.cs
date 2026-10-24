using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Combat;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectThreatModificationData : ISpellEffectThreatModificationData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public ThreatModificationType Type { get; private set; }
        public float Multiplier { get; private set; }
        public uint Amount { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry      = entry;
            Type       = (ThreatModificationType)entry.DataBits00;
            Multiplier = BitConverter.UInt32BitsToSingle(entry.DataBits01);
            Amount     = entry.DataBits03;
        }
    }
}
