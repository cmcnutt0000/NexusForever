using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Combat;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectUnitStateSetData : ISpellEffectUnitStateSetData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public UnitState State { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry = entry;
            State = (UnitState)entry.DataBits00;
        }
    }
}
