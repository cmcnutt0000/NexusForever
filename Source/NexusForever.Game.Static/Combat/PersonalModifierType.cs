namespace NexusForever.Game.Static.Combat
{
    /// <summary>
    /// What a PersonalDmgHealMod spell effect modifies, DataBits00 of the effect.
    /// </summary>
    /// <remarks>
    /// The bearer is the unit the effect is on. With DataBits01 1 the modifier only applies between the bearer and the caster of the effect.
    /// From the tooltips, eg: "Damage taken from the Stalker increased" (6, 7 and 8) and "Damage done to bonded Rootbrute increased" (3, 4 and 5).
    /// No effect uses 0 to 2, 10 or 11.
    /// </remarks>
    public enum PersonalModifierType
    {
        DamageDealtPhysical = 3,
        DamageDealtTech     = 4,
        DamageDealtMagic    = 5,
        DamageTakenPhysical = 6,
        DamageTakenTech     = 7,
        DamageTakenMagic    = 8,
        DamageTakenFlat     = 9,
        HealingDealt        = 12,
        HealingTaken        = 13
    }
}
