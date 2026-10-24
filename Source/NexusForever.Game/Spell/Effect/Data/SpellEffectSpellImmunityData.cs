using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Combat;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectSpellImmunityData : ISpellEffectSpellImmunityData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public SpellImmunityType Type { get; private set; }
        public uint Id { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry = entry;
            Type  = (SpellImmunityType)entry.DataBits00;
            Id    = entry.DataBits01;
        }
    }
}
