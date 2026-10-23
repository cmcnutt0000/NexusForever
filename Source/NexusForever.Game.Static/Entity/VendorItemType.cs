namespace NexusForever.Game.Static.Entity
{
    /// <summary>
    /// What a vendor entry sells, see 0x14047ABA0 in the client which rejects any other value.
    /// </summary>
    public enum VendorItemType : byte
    {
        Item     = 0,
        Spell    = 4,
        Currency = 5
    }
}
