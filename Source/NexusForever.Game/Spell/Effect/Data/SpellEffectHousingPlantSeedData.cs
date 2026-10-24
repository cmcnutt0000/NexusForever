using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectHousingPlantSeedData : ISpellEffectHousingPlantSeedData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint SeedId { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry  = entry;
            // 1 to 20, one per "Plant Seeds - <plant> (signal)" spell: 1 Octopod, 2 Spirovine, 3 Honeywheat ... 20 Heartichoke, no table has them
            SeedId = entry.DataBits00;
        }
    }
}
