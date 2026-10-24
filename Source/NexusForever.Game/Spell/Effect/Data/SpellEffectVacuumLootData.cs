using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectVacuumLootData : ISpellEffectVacuumLootData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public float Radius { get; private set; }
        public float Angle { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry  = entry;
            // 5, 30 and 50 on the Whimfiber Catcher, Proto-Coin and calldown reward spells, the angle is 360 when set
            Radius = BitConverter.UInt32BitsToSingle(entry.DataBits00);
            Angle  = BitConverter.UInt32BitsToSingle(entry.DataBits01);
        }
    }
}
