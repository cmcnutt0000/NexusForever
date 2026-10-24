using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Spell;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectAbsorptionData : ISpellEffectAbsorptionData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public float Multiplier { get; private set; }
        public float Amount { get; private set; }
        public float HitFraction { get; private set; }
        public uint DamageTypeMask { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry          = entry;
            Multiplier     = BitConverter.UInt32BitsToSingle(entry.DataBits00);
            Amount         = BitConverter.UInt32BitsToSingle(entry.DataBits01);
            HitFraction    = entry.DataBits02 != 0u ? entry.DataBits02 / 100f : 1f;
            DamageTypeMask = entry.DataBits04;
        }
    }
}
