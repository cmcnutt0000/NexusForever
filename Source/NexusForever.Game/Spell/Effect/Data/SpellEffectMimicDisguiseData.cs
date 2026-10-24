using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectMimicDisguiseData : ISpellEffectMimicDisguiseData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Unknown00 { get; private set; }
        public uint Unknown01 { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry     = entry;
            // both are 0 or 1, dataBits00 is 1 in 7 rows (the player clones and holograms, "Mimic Test"), dataBits01 in 17 (the same but "Mimic Test", the Stalker clone, "Mimic", "Projection")
            Unknown00 = entry.DataBits00;
            Unknown01 = entry.DataBits01;
        }
    }
}
