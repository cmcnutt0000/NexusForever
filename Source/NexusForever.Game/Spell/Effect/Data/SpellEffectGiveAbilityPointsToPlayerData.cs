using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectGiveAbilityPointsToPlayerData : ISpellEffectGiveAbilityPointsToPlayerData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Points { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry  = entry;
            Points = entry.DataBits00;
        }
    }
}
