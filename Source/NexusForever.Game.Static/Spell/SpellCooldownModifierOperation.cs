namespace NexusForever.Game.Static.Spell
{
    /// <summary>
    /// How a ModifySpellCooldown spell effect changes a cooldown, DataBits02 of the effect.
    /// </summary>
    /// <remarks>
    /// See 0x14046B920 in the client, DataBits03 is the multiplier or the milliseconds.
    /// </remarks>
    public enum SpellCooldownModifierOperation
    {
        /// <summary>
        /// Multiply the cooldown, it is never made longer.
        /// </summary>
        Reduce   = 0,
        Multiply = 1,

        /// <summary>
        /// Add milliseconds to the cooldown, it doesn't go below 0.
        /// </summary>
        Add      = 2,

        /// <summary>
        /// Set the cooldown in milliseconds.
        /// </summary>
        Set      = 3
    }
}
