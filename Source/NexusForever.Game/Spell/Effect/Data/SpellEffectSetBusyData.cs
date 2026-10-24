using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectSetBusyData : ISpellEffectSetBusyData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public bool Busy { get; private set; }
        public uint Unknown01 { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry     = entry;
            // 1 in 481 rows, 0 in 90: "Instant Activate + Busy (10 sec)" sets 1 and has a second effect 10000ms later that sets 0
            Busy      = entry.DataBits00 != 0u;
            // 14 rows, the same value on the effects that set and clear busy in a spell, eg: 11504 on both of "Eating Signal - Busy"
            Unknown01 = entry.DataBits01;
        }
    }
}
