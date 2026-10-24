namespace NexusForever.Game.Static.Spell
{
    /// <summary>
    /// Effect value a Spell4EffectModification row changes, see 0x1407A1C30 and 0x1407A2620 in the client.
    /// </summary>
    /// <remarks>
    /// 6 to 15 are DataBits00 to DataBits09, read with the field's type from SpellEffectType.tbl.
    /// The client also has 0 to 3 and 16 to 18 for fields the server doesn't know the names of.
    /// </remarks>
    public enum SpellEffectModificationParameter
    {
        DurationTime = 4,
        Flags        = 5,
        DataBits00   = 6,
        DataBits09   = 15
    }
}
