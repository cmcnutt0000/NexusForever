using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectSummonTrapData : ISpellEffectSummonTrapData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint CreatureId { get; private set; }
        public uint SpellId { get; private set; }
        public TimeSpan ArmTime { get; private set; }
        public float Radius { get; private set; }
        public uint PrerequisiteId { get; private set; }
        public bool EnemiesOnly { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry          = entry;
            CreatureId     = entry.DataBits00;
            SpellId        = entry.DataBits01;
            ArmTime        = TimeSpan.FromMilliseconds(entry.DataBits02);
            Radius         = BitConverter.UInt32BitsToSingle(entry.DataBits04);
            PrerequisiteId = entry.DataBits05;
            // 0 on the mines (Proximity Mine, Tether Mine), 1 on the heals and quest traps whose prerequisite picks the unit (Mirage, Rejuvenator)
            EnemiesOnly    = entry.DataBits06 == 0u;
        }
    }
}
