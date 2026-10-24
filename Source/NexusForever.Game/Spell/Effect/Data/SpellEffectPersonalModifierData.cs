using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Combat;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectPersonalModifierData : ISpellEffectPersonalModifierData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public PersonalModifierType Type { get; private set; }
        public bool CasterOnly { get; private set; }
        public float Multiplier { get; private set; }
        public uint FlatValue { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry      = entry;
            Type       = (PersonalModifierType)entry.DataBits00;
            CasterOnly = entry.DataBits01 == 1u;
            Multiplier = BitConverter.UInt32BitsToSingle(entry.DataBits02);
            FlatValue  = entry.DataBits03;
        }
    }
}
