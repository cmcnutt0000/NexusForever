using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectQuestAdvanceObjectiveData : ISpellEffectQuestAdvanceObjectiveData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint QuestId { get; private set; }
        public byte ObjectiveIndex { get; private set; }
        public uint Count { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry          = entry;
            QuestId        = entry.DataBits00;
            // index into the quest's objectives, 0-5
            ObjectiveIndex = (byte)entry.DataBits01;
            Count          = entry.DataBits02;
        }
    }
}
