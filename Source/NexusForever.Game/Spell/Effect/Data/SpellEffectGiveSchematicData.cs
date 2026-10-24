using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectGiveSchematicData : ISpellEffectGiveSchematicData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint SchematicId { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry       = entry;
            // TradeskillSchematic2 id, 786 of the 1,171 effects name schematics that are no longer in the table
            SchematicId = entry.DataBits00;
        }
    }
}
