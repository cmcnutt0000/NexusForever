using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectModifyAbilityChargesData : ISpellEffectModifyAbilityChargesData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Spell4BaseId { get; private set; }
        public int Charges { get; private set; }
        public bool MaximumCharges { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry          = entry;
            Spell4BaseId   = entry.DataBits00;
            Charges        = (int)entry.DataBits01;
            MaximumCharges = entry.DataBits02 == 1u;
        }
    }
}
