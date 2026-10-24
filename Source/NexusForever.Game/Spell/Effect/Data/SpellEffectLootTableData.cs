using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectLootTableData : ISpellEffectLootTableData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint LootTableId { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry       = entry;
            // loot tables are server data, no client table matches (nine Winterfest "Give ..." spells share 46556)
            LootTableId = entry.DataBits00;
        }
    }
}
