namespace NexusForever.Game.Static.Spell
{
    /// <summary>
    /// Spell value a Spell4Modification row changes, see 0x1407A18C0 and 0x1407A1EB0 in the client.
    /// </summary>
    /// <remarks>
    /// The client stores 22, only the ones the server uses are named, the columns are from the client's copy of the Spell4 row (0x140568540).
    /// Threat (13) has no value in the client, its rows are named from the tooltips.
    /// </remarks>
    public enum SpellModificationParameter
    {
        CastTime     = 0,
        MinimumRange = 1,
        MaximumRange = 2,
        Cooldown     = 3,
        InnateCost0  = 8,
        InnateCost1  = 9,
        Threat       = 13
    }
}
