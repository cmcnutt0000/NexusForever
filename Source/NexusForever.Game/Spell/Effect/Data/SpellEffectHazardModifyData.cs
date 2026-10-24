using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Hazard;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectHazardModifyData : ISpellEffectHazardModifyData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public HazardType HazardType { get; private set; }
        public uint Unknown01 { get; private set; }
        public float Unknown02 { get; private set; }
        public float Amount { get; private set; }
        public uint HazardId { get; private set; }
        public uint Unknown06 { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry      = entry;
            // inferred, it is the hazardTypeEnum of the dataBits04 hazard in 43 of the 49 rows, the others are 3 "Clean Air" rows (4) and 3 of hazard 249 (0)
            HazardType = (HazardType)entry.DataBits00;
            // 1 in 46 rows, 2 in the 3 "Clean Air" rows, which also have 0.07 in dataBits02
            Unknown01  = entry.DataBits01;
            Unknown02  = BitConverter.UInt32BitsToSingle(entry.DataBits02);
            // tooltips: "Reduces Dehydration by 30." is -30 on hazard 163, "Increases Molten Heat by $(eff=174183.data03) every $(eff=174183.tickTimeMS)s."
            Amount     = BitConverter.UInt32BitsToSingle(entry.DataBits03);
            // all 49 are Hazard ids
            HazardId   = entry.DataBits04;
            // 3 in 5 rows
            Unknown06  = entry.DataBits06;
        }
    }
}
