using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectProxyChannelData : ISpellEffectProxyChannelData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public IReadOnlyList<uint> Spells { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry  = entry;
            Spells = [entry.DataBits00, entry.DataBits01, entry.DataBits02, entry.DataBits03, entry.DataBits04, entry.DataBits05];
        }
    }
}
