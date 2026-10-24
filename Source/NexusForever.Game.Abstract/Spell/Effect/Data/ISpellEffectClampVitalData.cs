namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// From the tooltips, eg: "This creature can't die by natural means" (minimum of 1%) and "Maximum current health reduced by 5% per stack" (ceiling).
    /// </remarks>
    public interface ISpellEffectClampVitalData : ISpellEffectData
    {
        /// <summary>
        /// Clamp the maximum instead of the minimum health.
        /// </summary>
        bool Ceiling { get; }

        /// <summary>
        /// Fraction of maximum health, the minimum health or the reduction of the ceiling.
        /// </summary>
        float Fraction { get; }

        /// <summary>
        /// Each stack reduces the ceiling.
        /// </summary>
        bool PerStack { get; }
    }
}
