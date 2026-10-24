namespace NexusForever.Game.Static.Combat
{
    /// <summary>
    /// State set on a unit by a UnitStateSet spell effect, DataBits00 of the effect.
    /// </summary>
    /// <remarks>
    /// The client validates the state is no more than 23, see 0x140567AF0 in the client.
    /// Only the states with a known meaning are named, the others are still tracked.
    /// </remarks>
    public enum UnitState
    {
        Invulnerable = 1,
        DamageImmune = 22
    }
}
