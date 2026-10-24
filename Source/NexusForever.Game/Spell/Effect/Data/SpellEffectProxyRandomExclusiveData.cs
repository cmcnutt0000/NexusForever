using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectProxyRandomExclusiveData : ISpellEffectProxyRandomExclusiveData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public IReadOnlyList<(uint Spell4Id, uint Weight)> Spells { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry  = entry;
            Spells = new (uint, uint)[]
            {
                (entry.DataBits00, entry.DataBits01),
                (entry.DataBits02, entry.DataBits03),
                (entry.DataBits04, entry.DataBits05),
                (entry.DataBits06, entry.DataBits07),
                (entry.DataBits08, entry.DataBits09)
            }
            .Where(s => s.Item1 != 0u)
            .ToList();
        }
    }
}
