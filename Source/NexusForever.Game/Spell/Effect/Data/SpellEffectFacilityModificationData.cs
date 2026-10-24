using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectFacilityModificationData : ISpellEffectFacilityModificationData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint FacilityId { get; private set; }
        public int Unknown01 { get; private set; }
        public uint Operation { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry      = entry;
            // 403 ids that no table has, spell names call them facilities: "Requires creature 18728 with facility 6776" is 6776 here
            FacilityId = entry.DataBits00;
            // signed, 48 rows: 1, 2, 9, 2500, 3000, 30000 ...
            Unknown01  = (int)entry.DataBits01;
            // inferred from spell names: 3 removes ("Remove Facility", "Blood Extractor Facility remove", "Remove Charge Facility"),
            // 0 ("Q4950 - Add Facility") and 1 ("Hunt Facility", "Gain Healer Facility") add, 2 is on the "Requires creature 18728 with facility 6776" missiles
            // dataBits03 (1 in 51 rows), dataBits04 (-1 in 20) and dataBits05 (60 or 1000 with it) look like the targeting fields Proxy has there
            Operation  = entry.DataBits02;
        }
    }
}
