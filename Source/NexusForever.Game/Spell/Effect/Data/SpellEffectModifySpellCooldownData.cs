using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Spell;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectModifySpellCooldownData : ISpellEffectModifySpellCooldownData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public SpellCooldownModifierType Type { get; private set; }
        public uint Id { get; private set; }
        public SpellCooldownModifierOperation Operation { get; private set; }
        public float Amount { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry     = entry;
            Type      = (SpellCooldownModifierType)entry.DataBits00;
            Id        = entry.DataBits01;
            Operation = (SpellCooldownModifierOperation)entry.DataBits02;
            Amount    = BitConverter.UInt32BitsToSingle(entry.DataBits03);
        }
    }
}
