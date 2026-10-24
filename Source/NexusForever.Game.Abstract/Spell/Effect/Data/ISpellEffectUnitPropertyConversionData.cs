using NexusForever.Game.Static.Entity;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectUnitPropertyConversionData : ISpellEffectData
    {
        /// <summary>
        /// <see cref="Static.Entity.Property"/> whose value is converted.
        /// </summary>
        Property SourceProperty { get; }

        /// <summary>
        /// <see cref="Static.Entity.Property"/> that gains the converted value.
        /// </summary>
        Property Property { get; }

        /// <summary>
        /// Fraction of <see cref="SourceProperty"/> added to <see cref="Property"/>.
        /// </summary>
        float Fraction { get; }
    }
}
