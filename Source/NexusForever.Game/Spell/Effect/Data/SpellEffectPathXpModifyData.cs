using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectPathXpModifyData : ISpellEffectPathXpModifyData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public int Amount { get; private set; }
        public bool IsLevel { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry   = entry;
            Amount  = (int)entry.DataBits00;
            // set on the two "Path Level Up" spells (1 and 30), Amount is levels rather than experience then
            IsLevel = entry.DataBits01 != 0u;
        }
    }
}
