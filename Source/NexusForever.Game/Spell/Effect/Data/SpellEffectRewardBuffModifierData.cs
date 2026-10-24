using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectRewardBuffModifierData : ISpellEffectRewardBuffModifierData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint ContentType { get; private set; }
        public float Unknown01 { get; private set; }
        public float Unknown02 { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry       = entry;
            // 1 PvP, 2 group PvE and 3 world PvE by the names of the three "[Test] Daily Reward Buff" spells
            ContentType = entry.DataBits00;
            Unknown01   = BitConverter.UInt32BitsToSingle(entry.DataBits01);
            Unknown02   = BitConverter.UInt32BitsToSingle(entry.DataBits02);
        }
    }
}
