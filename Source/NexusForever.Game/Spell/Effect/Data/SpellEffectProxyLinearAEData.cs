using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectProxyLinearAEData : ISpellEffectProxyLinearAEData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Spell4Id { get; private set; }
        public float Angle { get; private set; }
        public float Distance { get; private set; }
        public uint Count { get; private set; }
        public uint Interval { get; private set; }
        public float Spacing { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry    = entry;
            Spell4Id = entry.DataBits00;
            Angle    = BitConverter.UInt32BitsToSingle(entry.DataBits01);
            Distance = BitConverter.UInt32BitsToSingle(entry.DataBits02);
            Count    = Math.Max(1u, entry.DataBits03);
            Interval = entry.DataBits04;
            Spacing  = BitConverter.UInt32BitsToSingle(entry.DataBits06);
        }
    }
}
