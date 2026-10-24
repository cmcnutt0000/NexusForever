using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectSettlerCampfireData : ISpellEffectSettlerCampfireData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Tier { get; private set; }
        public uint Unknown01 { get; private set; }
        public uint Unknown02 { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry     = entry;
            // 0, 1 and 2 in "Settler's Campfire - Base - Tier 1" to "Tier 3"
            Tier      = entry.DataBits00;
            // 91 and 1 in all 3 rows, 91 isn't a Creature2 id
            Unknown01 = entry.DataBits01;
            Unknown02 = entry.DataBits02;
        }
    }
}
