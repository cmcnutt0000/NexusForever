using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectSetMatchingEligibilityData : ISpellEffectSetMatchingEligibilityData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint EligibilityType { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry           = entry;
            // by the tooltips of the 8 rows: 0 "You cannot queue for a PvP match." (PvP Deserter), 1 "You cannot queue for Dungeons or Adventures." (PvE Deserter)
            // and 15 "You cannot queue for Expeditions." (Shiphand Deserter), it isn't a MatchType (1 is only Dungeon there)
            EligibilityType = entry.DataBits00;
        }
    }
}
