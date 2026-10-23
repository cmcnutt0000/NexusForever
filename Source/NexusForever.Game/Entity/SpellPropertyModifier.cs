using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Static.Entity;

namespace NexusForever.Game.Entity
{
    public class SpellPropertyModifier : ISpellPropertyModifier
    {
        public Property Property { get; }
        public uint Priority { get; }
        public List<IPropertyModifier> Alterations { get; } = new();
        public uint StackCount { get; } // TODO: Should we have StackCount on this? I presume we just want to have spell effects stack up individually, not tracked in each SpellPropertyModifier

        public SpellPropertyModifier(Property property, uint priority, float value2, float value3, float value4, uint stackCount = 1)
        {
            Property   = property;
            Priority   = priority;
            StackCount = stackCount;

            CalculateModAndValue(value2, value3, value4);
        }

        /// <summary>
        /// Calculates the modifier type and value.
        /// </summary>
        /// <remarks>
        /// Derived from the Spell4Effects data and tooltips, the client doesn't calculate unit properties itself:
        /// Value2 multiplies the property and a value of 0 is used to zero it (move speed during pulls and leaps, falling damage) or, with Value3, to set it (interrupt armour -1 for Unstoppable Force).
        /// Value3 is added when Value2 is 1 or 0. The 131 effects with both set otherwise are ignoring Value3, it is mostly a placeholder of 1 there and tooltips such as Zeal's describe only the percentage.
        /// Value4 is a further value per level, used by very few spells.
        /// </remarks>
        private void CalculateModAndValue(float value2, float value3, float value4)
        {
            if (value2 != 1f)
                Alterations.Add(new PropertyModifier(Property, ModType.Percentage, value2));

            if (value3 != 0f && (value2 == 1f || value2 == 0f))
                Alterations.Add(new PropertyModifier(Property, ModType.FlatValue, value3));

            if (value4 != 0f)
                Alterations.Add(new PropertyModifier(Property, ModType.LevelScale, value4));
        }
    }
}
