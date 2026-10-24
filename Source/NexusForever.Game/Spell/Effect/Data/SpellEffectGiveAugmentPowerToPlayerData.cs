using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectGiveAugmentPowerToPlayerData : ISpellEffectGiveAugmentPowerToPlayerData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Power { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry = entry;
            Power = entry.DataBits00;
        }
    }
}
