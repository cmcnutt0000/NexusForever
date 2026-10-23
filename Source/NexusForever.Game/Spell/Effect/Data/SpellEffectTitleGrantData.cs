using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectTitleGrantData : ISpellEffectTitleGrantData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint TitleId { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry   = entry;
            TitleId = entry.DataBits00;
        }
    }
}
