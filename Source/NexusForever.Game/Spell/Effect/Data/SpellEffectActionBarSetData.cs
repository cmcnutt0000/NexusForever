using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectActionBarSetData : ISpellEffectActionBarSetData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint ShortcutSetId { get; private set; }
        public uint Unknown01 { get; private set; }
        public uint Unknown02 { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry         = entry;
            // all 405 rows are ActionBarShortcutSet ids, eg: 2 "[PvZ] Turret Action Bar"
            ShortcutSetId = entry.DataBits00;
            // 5 rows have a second ActionBarShortcutSet id (194, 166, 144 and 1), eg: the two cinematics manager bars
            Unknown01     = entry.DataBits01;
            // 0 in 308 rows, 1 in 96, 2 in 1, it may pick the bar
            // dataBits03 to dataBits05 (1.0, -1, 1000 in 12 rows) look like the targeting fields Proxy has there
            Unknown02     = entry.DataBits02;
        }
    }
}
