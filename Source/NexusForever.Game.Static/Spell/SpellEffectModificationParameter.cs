namespace NexusForever.Game.Static.Spell
{
    /// <summary>
    /// Effect value a Spell4EffectModification row changes, see 0x1407A1C30 and 0x1407A2620 in the client.
    /// </summary>
    /// <remarks>
    /// The fields of the client's copy of the Spell4Effects row, which 0x140567950 fills, see 0x1407A1C30.
    /// 6 to 15 are DataBits00 to DataBits09, read with the field's type from SpellEffectType.tbl.
    /// </remarks>
    public enum SpellEffectModificationParameter
    {
        TargetFlags        = 0,
        DamageType         = 1,
        DelayTime          = 2,
        TickTime           = 3,
        DurationTime       = 4,
        Flags              = 5,
        DataBits00         = 6,
        DataBits09         = 15,
        InnateCostPerTick0 = 16,
        InnateCostPerTick1 = 17,
        ThreatMultiplier   = 18
    }
}
