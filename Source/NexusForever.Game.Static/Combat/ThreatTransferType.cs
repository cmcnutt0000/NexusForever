namespace NexusForever.Game.Static.Combat
{
    /// <summary>
    /// How a ThreatTransfer spell effect moves threat, DataBits00 of the effect.
    /// </summary>
    /// <remarks>
    /// From the tooltips: Warrior Battlefield Commander "party members ... will transfer 75% of their threat" and Engineer Code Red (0),
    /// Spellslinger Subtlety "transfers 30% of all threat you generate to your allied target" (1), Engineer "Pet - Transfer Threat to Master" (3).
    /// The 2 effects of type 2 aren't known.
    /// </remarks>
    public enum ThreatTransferType
    {
        /// <summary>
        /// Part of the threat the bearer generates goes to the caster while the effect is active.
        /// </summary>
        BearerToCaster   = 0,

        /// <summary>
        /// Part of the threat the caster generates goes to the bearer while the effect is active.
        /// </summary>
        CasterToBearer   = 1,

        /// <summary>
        /// Part of the caster's threat on the target moves to the caster's summoner.
        /// </summary>
        CasterToSummoner = 3
    }
}
