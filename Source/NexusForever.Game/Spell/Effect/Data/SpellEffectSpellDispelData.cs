using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Spell;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectSpellDispelData : ISpellEffectSpellDispelData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Count { get; private set; }
        public SpellClass SpellClass { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry      = entry;
            Count      = entry.DataBits00;
            SpellClass = (SpellClass)entry.DataBits03;
        }
    }
}
