using System.Numerics;
using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if something is equipped in the equipment slot of the ItemSlot in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x14049D2C0 in the client, the slot is the lowest bit of ItemSlot.equippedSlotFlags.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ItemSlotEquipped)]
    public class PrerequisiteCheckItemSlotEquipped : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItemSlotEquipped> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckItemSlotEquipped(
            ILogger<PrerequisiteCheckItemSlotEquipped> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            ItemSlotEntry entry = gameTableManager.ItemSlot.GetEntry(objectId);
            if (entry == null || entry.EquippedSlotFlags == 0u)
                return false;

            uint bagIndex = (uint)BitOperations.TrailingZeroCount(entry.EquippedSlotFlags);

            if (!PrerequisiteComparer.TryEvaluate(comparison, player.Inventory.GetItem(InventoryLocation.Equipped, bagIndex) != null, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ItemSlotEquipped}!");

            return result;
        }
    }
}
