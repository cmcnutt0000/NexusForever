using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectDisembarkData : ISpellEffectDisembarkData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public SpellEffectDisembarkFlags Flags { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry = entry;
            Flags = (SpellEffectDisembarkFlags)entry.DataBits00;
        }
    }
}
