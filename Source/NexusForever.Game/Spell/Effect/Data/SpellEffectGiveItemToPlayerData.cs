using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectGiveItemToPlayerData : ISpellEffectGiveItemToPlayerData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint ItemId { get; private set; }
        public uint Count { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry  = entry;
            ItemId = entry.DataBits00;
            Count  = entry.DataBits01;
        }
    }
}
