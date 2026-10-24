using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectUnlockActionBarData : ISpellEffectUnlockActionBarData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public byte ActionSetIndex { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry          = entry;
            // the client's effect check (0x140567AF0) wants 3 or less, the 4 rows are 0 to 3 for "Action Set Unlock" and "Action Set 2 Unlock" to "Action Set 4 Unlock"
            ActionSetIndex = (byte)entry.DataBits00;
        }
    }
}
