using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectMiniMapIconData : ISpellEffectMiniMapIconData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Unknown00 { get; private set; }
        public uint Unknown01 { get; private set; }
        public uint Unknown02 { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry     = entry;
            // 3 rows: "Track Player" has two effects, 3 and 4, likely one per faction (inferred), a nameless spell has 59
            // they aren't MiniMapMarker ids (3 and 4 are IronNode and TitaniumNode there)
            Unknown00 = entry.DataBits00;
            // 1 and 1 on both "Track Player" rows
            Unknown01 = entry.DataBits01;
            Unknown02 = entry.DataBits02;
        }
    }
}
