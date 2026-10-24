namespace NexusForever.Game.Static.Combat
{
    /// <summary>
    /// How a ThreatModification spell effect changes threat, DataBits00 of the effect.
    /// </summary>
    /// <remarks>
    /// From the effects, eg: Medic Calm multiplies by 0.75 ("Reduces your Threat against affected foes by 25%"), Stalker "Drop Threat" drops and Brawler "Wipe Threat" reduces by 100%.
    /// No effect uses 3 or 5.
    /// </remarks>
    public enum ThreatModificationType
    {
        Multiply      = 0,
        ReducePercent = 1,
        Drop          = 2,
        Add           = 4,
        Wipe          = 6
    }
}
