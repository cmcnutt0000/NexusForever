using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectChangeDisplayNameData : ISpellEffectChangeDisplayNameData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint LocalizedTextId { get; private set; }
        public uint Unknown02 { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry           = entry;
            // the client (0x1405D9DB0) looks the name up as localised text, 234 of the 268 ids are in the en-US text, eg: 162976 "Altered Rockhound Slasher" for "Altercation Beam"
            LocalizedTextId = entry.DataBits00;
            // 39983 in 13 rows, 4000, 32789 and 32790, the client doesn't read it
            Unknown02       = entry.DataBits02;
        }
    }
}
