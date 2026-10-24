using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectHazardSuspendData : ISpellEffectHazardSuspendData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint HazardId { get; private set; }
        public uint Unknown01 { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry     = entry;
            // all 20 are Hazard ids, eg: 71 and 72 for "Hazmat Protection: Prevents poisoning for 30 minutes."
            HazardId  = entry.DataBits00;
            // 2 in 6 rows
            Unknown01 = entry.DataBits01;
        }
    }
}
