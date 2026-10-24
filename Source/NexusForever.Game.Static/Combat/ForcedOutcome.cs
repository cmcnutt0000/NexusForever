namespace NexusForever.Game.Static.Combat
{
    /// <summary>
    /// Outcome forced on an attack by a ForcedAction spell effect, DataBits00 of the effect.
    /// </summary>
    /// <remarks>
    /// From the tooltips of the 95 effects: 0 "chance to deflect", 1 "attack automatically misses", 2 "will automatically fierce", 3 "chance to hit".
    /// </remarks>
    public enum ForcedOutcome
    {
        Deflect  = 0,
        Miss     = 1,
        Critical = 2,
        Hit      = 3
    }
}
