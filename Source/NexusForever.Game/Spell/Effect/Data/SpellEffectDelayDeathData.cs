using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectDelayDeathData : ISpellEffectDelayDeathData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Spell4Id { get; private set; }
        public uint PrerequisiteId { get; private set; }
        public uint Cooldown { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry          = entry;
            Spell4Id       = entry.DataBits01;
            PrerequisiteId = entry.DataBits03;
            Cooldown       = entry.DataBits04;
        }
    }
}
