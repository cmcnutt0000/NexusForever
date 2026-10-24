using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectHousingTeleportData : ISpellEffectHousingTeleportData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Unknown00 { get; private set; }
        public uint Unknown01 { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry     = entry;
            // 0 in "Recall - House" ("Teleports you to your housing property.") and the two "Teleport to the Skymap" spells,
            // 2 in 4 old "Auto-Attack Starter" spells
            Unknown00 = entry.DataBits00;
            // 1 in the two settler "Report Home" spells
            Unknown01 = entry.DataBits01;
        }
    }
}
