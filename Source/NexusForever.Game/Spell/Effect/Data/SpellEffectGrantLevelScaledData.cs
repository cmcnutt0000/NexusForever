using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectGrantLevelScaledData : ISpellEffectGrantLevelScaledData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public float Multiplier { get; private set; }
        public uint MaxLevel { get; private set; }
        public uint MinLevel { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry      = entry;
            Multiplier = BitConverter.UInt32BitsToSingle(entry.DataBits00);
            // 50 and 1 on every effect, as for GrantXP
            MaxLevel   = entry.DataBits01;
            MinLevel   = entry.DataBits02;
        }
    }
}
