using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Combat;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectForcedActionData : ISpellEffectForcedActionData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public ForcedOutcome Outcome { get; private set; }
        public int Count { get; private set; }
        public bool AgainstBearer { get; private set; }
        public float Chance { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry         = entry;
            Outcome       = (ForcedOutcome)entry.DataBits00;
            Count         = (int)entry.DataBits01;
            AgainstBearer = entry.DataBits02 == 1u;
            Chance        = GetChance(entry.DataBits03);
        }

        private static float GetChance(uint dataBits)
        {
            // 43 effects store an integer 1 instead of a float, the tooltips say these are 100%
            if (dataBits is > 0u and < 0x00800000u)
                return 1f;

            float chance = BitConverter.UInt32BitsToSingle(dataBits);
            return chance > 0f ? chance : 1f;
        }
    }
}
