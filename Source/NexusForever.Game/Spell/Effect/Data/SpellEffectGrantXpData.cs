using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectGrantXpData : ISpellEffectGrantXpData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Xp { get; private set; }
        public uint MaxLevel { get; private set; }
        public uint MinLevel { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry    = entry;
            Xp       = entry.DataBits00;
            // 50 and 1 on every effect of this type, GrantLevelScaledXP and GrantLevelScaledPrestige
            MaxLevel = entry.DataBits01;
            MinLevel = entry.DataBits02;
        }
    }
}
