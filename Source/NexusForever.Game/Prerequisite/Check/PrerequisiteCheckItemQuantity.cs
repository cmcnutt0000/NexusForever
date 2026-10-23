using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the count of the item in objectId the player has equipped or carried with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049D280 in the client, mask 0x303 as <see cref="PrerequisiteCheckItemOnCharacter"/>.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ItemQuantity)]
    public class PrerequisiteCheckItemQuantity : IPrerequisiteCheck
    {
        private static readonly InventoryLocation[] locations =
        {
            InventoryLocation.Equipped,
            InventoryLocation.Inventory,
            InventoryLocation.Unknown8,
            InventoryLocation.Unknown9
        };

        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItemQuantity> log;

        public PrerequisiteCheckItemQuantity(
            ILogger<PrerequisiteCheckItemQuantity> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            uint count = player.GetItemCount(objectId, locations);

            if (!PrerequisiteComparer.TryCompare(comparison, count, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ItemQuantity}!");

            return result;
        }
    }
}
