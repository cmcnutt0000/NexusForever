using NexusForever.Game.Static.Combat;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectUnitStateSetData : ISpellEffectData
    {
        /// <summary>
        /// State set on the bearer.
        /// </summary>
        UnitState State { get; }
    }
}
