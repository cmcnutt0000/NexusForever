using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectAchievementAdvanceData : ISpellEffectAchievementAdvanceData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint AchievementId { get; private set; }
        public uint Count { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry         = entry;
            AchievementId = entry.DataBits00;
            Count         = entry.DataBits01;
        }
    }
}
