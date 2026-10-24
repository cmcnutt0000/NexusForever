using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Spell;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectActionBarSetData : ISpellEffectActionBarSetData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint ShortcutSetId { get; private set; }
        public uint Unknown01 { get; private set; }
        public ShortcutSet Bar { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry         = entry;
            // all 405 rows are ActionBarShortcutSet ids, eg: 2 "[PvZ] Turret Action Bar"
            ShortcutSetId = entry.DataBits00;
            // 5 rows have a second ActionBarShortcutSet id (194, 166, 144 and 1), eg: the two cinematics manager bars
            Unknown01     = entry.DataBits01;
            // 0 in 308 rows (vehicles, turrets, harnesses), 1 in 96 ("Carrying Pipe", "Carrying Turret") and 2 in 1;
            // the client only treats the vehicle bar (0) and the two floating bars (7, 8) apart when it shows a set (0x1403B9920), so these are taken to be those three (inferred)
            // dataBits03 to dataBits05 (1.0, -1, 1000 in 12 rows) look like the targeting fields Proxy has there
            Bar           = entry.DataBits02 switch
            {
                1u => ShortcutSet.FloatingSpellBar,
                2u => ShortcutSet.FloatingDynamicSpellBar,
                _  => ShortcutSet.VehicleBar
            };
        }
    }
}
