using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectChangeIconData : ISpellEffectChangeIconData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Spell4Id { get; private set; }
        public uint IconSpell4Id { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry        = entry;
            // the client (0x1405D9A20) keeps dataBits00 as the spell to change and takes the icon of the dataBits01 spell when it is set,
            // all 23 + 23 values are Spell4 ids, eg: "Shred Icon 1" shows spell 23828's icon on spell 23827
            Spell4Id     = entry.DataBits00;
            IconSpell4Id = entry.DataBits01;
        }
    }
}
