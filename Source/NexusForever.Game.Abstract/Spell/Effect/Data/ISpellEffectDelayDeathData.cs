namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// Warrior "To the Pain": "An attack that would normally kill you will instead set you to 30% of your maximum Health".
    /// </remarks>
    public interface ISpellEffectDelayDeathData : ISpellEffectData
    {
        /// <summary>
        /// Spell cast by the bearer instead of dying.
        /// </summary>
        uint Spell4Id { get; }

        /// <summary>
        /// Prerequisite the bearer must meet.
        /// </summary>
        uint PrerequisiteId { get; }

        /// <summary>
        /// Time in milliseconds before the effect can trigger again.
        /// </summary>
        uint Cooldown { get; }
    }
}
