using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectProxyChannelVariableTimeData : ISpellEffectProxyChannelVariableTimeData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public IReadOnlyList<(uint Spell4Id, uint Delay)> Spells { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry = entry;

            // up to 5 spells with the gap before each of the next 4 between them, eg: Gnashing Frenzy 4 spells with gaps of 666, 667 and 867
            uint[] spells = [entry.DataBits00, entry.DataBits02, entry.DataBits04, entry.DataBits06, entry.DataBits08];
            uint[] gaps   = [entry.DataBits01, entry.DataBits03, entry.DataBits05, entry.DataBits07];

            var list = new List<(uint, uint)>();
            uint delay = 0u;
            for (int i = 0; i < spells.Length; i++)
            {
                if (i > 0)
                    delay += gaps[i - 1];
                if (spells[i] != 0u)
                    list.Add((spells[i], delay));
            }

            Spells = list;
        }
    }
}
