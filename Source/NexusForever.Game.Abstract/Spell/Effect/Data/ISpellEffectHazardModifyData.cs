using NexusForever.Game.Static.Hazard;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectHazardModifyData : ISpellEffectData
    {
        HazardType HazardType { get; }
        uint Unknown01 { get; }
        float Unknown02 { get; }

        /// <summary>
        /// Change of the hazard's meter, eg: -30 for "Reduces Dehydration by 30."
        /// </summary>
        float Amount { get; }

        uint HazardId { get; }
        uint Unknown06 { get; }
    }
}
