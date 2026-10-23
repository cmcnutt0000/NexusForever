namespace NexusForever.Game.Static.Entity
{
    /// <summary>
    /// See Unit:GetVendorItems (0x14065A560) in the client.
    /// </summary>
    [Flags]
    public enum VendorItemFlags : uint
    {
        None                     = 0x00,
        HideIfPrerequisiteNotMet = 0x01,
        Special                  = 0x02,
        FutureStock              = 0x04
    }
}
