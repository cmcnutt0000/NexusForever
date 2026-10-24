using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectPetCastSpellData : ISpellEffectPetCastSpellData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint PetSpellId { get; private set; }
        public uint SpellId { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry      = entry;
            PetSpellId = entry.DataBits00;
            SpellId    = entry.DataBits01;
        }
    }
}
