namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// The only effect, Medic "Fusion Probe - Remove Fluff", starts the cooldown of base spell 63410; DataBits02 is 1 and not known.
    /// </remarks>
    public interface ISpellEffectActivateSpellCooldownData : ISpellEffectData
    {
        /// <summary>
        /// Base spell whose cooldown is started.
        /// </summary>
        uint Spell4BaseId { get; }
    }
}
