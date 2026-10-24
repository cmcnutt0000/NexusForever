using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectHazardEnableData : ISpellEffectHazardEnableData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint HazardId { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry    = entry;
            // all 20 are Hazard ids, eg: 101 for "Poison Gas", 99 for the flashlight helmet "EVA XR-51"
            HazardId = entry.DataBits00;
        }
    }
}
