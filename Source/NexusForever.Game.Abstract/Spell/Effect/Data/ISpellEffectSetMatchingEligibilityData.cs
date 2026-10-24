namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectSetMatchingEligibilityData : ISpellEffectData
    {
        /// <summary>
        /// Kind of content the target can't queue for, see the remarks of the data class.
        /// </summary>
        uint EligibilityType { get; }
    }
}
