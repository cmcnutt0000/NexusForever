using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Static.Entity;

namespace NexusForever.Game.Prerequisite
{
    public static class InventoryPrerequisiteExtensions
    {
        /// <summary>
        /// Return the count of items with the supplied id in the supplied <see cref="InventoryLocation"/>s.
        /// </summary>
        /// <remarks>
        /// See 0x1403AC840 in the client, which takes a mask of 1 &lt;&lt; <see cref="InventoryLocation"/>.
        /// The supply satchel is counted with <see cref="InventoryLocation.Inventory"/>.
        /// </remarks>
        public static uint GetItemCount(this IPlayer player, uint itemId, IReadOnlyCollection<InventoryLocation> locations)
        {
            uint count = 0u;
            foreach (IBag bag in player.Inventory.Where(b => locations.Contains(b.Location)))
            {
                // the client counts an item without a stack count as 1
                foreach (IItem item in bag.Where(i => i.Id == itemId))
                    count += Math.Max(1u, item.StackCount);
            }

            if (locations.Contains(InventoryLocation.Inventory))
                count += player.SupplySatchelManager.GetAmount(itemId);

            return count;
        }
    }
}
