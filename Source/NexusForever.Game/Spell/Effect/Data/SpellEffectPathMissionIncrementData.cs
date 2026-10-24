using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectPathMissionIncrementData : ISpellEffectPathMissionIncrementData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint PathMissionId { get; private set; }
        public uint Count { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry         = entry;
            // missions 2424 and 3085, named in their spells ("M2424 - Illium - SWAT")
            PathMissionId = entry.DataBits00;
            Count         = entry.DataBits01;
        }
    }
}
