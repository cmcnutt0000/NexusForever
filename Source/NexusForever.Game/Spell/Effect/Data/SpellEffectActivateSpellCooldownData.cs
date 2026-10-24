using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectActivateSpellCooldownData : ISpellEffectActivateSpellCooldownData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Spell4BaseId { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry        = entry;
            Spell4BaseId = entry.DataBits01;
        }
    }
}
