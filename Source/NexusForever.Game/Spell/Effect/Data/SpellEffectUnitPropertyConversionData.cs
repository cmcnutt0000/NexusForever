using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Entity;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectUnitPropertyConversionData : ISpellEffectUnitPropertyConversionData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public Property SourceProperty { get; private set; }
        public Property Property { get; private set; }
        public float Fraction { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry          = entry;
            // by the tooltips of the 6 rows: "Increase your Armor by $(eff=215867.data02pct) of your Max Health." is 7 (BaseHealth) to 42 (Armor),
            // "Increase your Multi-Hit Severity rating by $(eff=157947.data02pct) of your Critical Hit Severity rating." is 47 to 60
            SourceProperty = (Property)entry.DataBits00;
            Property       = (Property)entry.DataBits01;
            // 0.01 to 0.5, shown as a percentage
            Fraction       = BitConverter.UInt32BitsToSingle(entry.DataBits02);
        }
    }
}
