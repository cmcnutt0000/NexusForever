using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player has the item in value equipped or carried.
    /// </summary>
    /// <remarks>
    /// See 0x14049D200 in the client, mask 0x303 which includes the supply satchel. NexusForever doesn't create bags for 8 and 9 yet.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ItemOnCharacter)]
    public class PrerequisiteCheckItemOnCharacter : IPrerequisiteCheck
    {
        private static readonly InventoryLocation[] locations =
        {
            InventoryLocation.Equipped,
            InventoryLocation.Inventory,
            InventoryLocation.Unknown8,
            InventoryLocation.Unknown9
        };

        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItemOnCharacter> log;

        public PrerequisiteCheckItemOnCharacter(
            ILogger<PrerequisiteCheckItemOnCharacter> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            bool hasItem = player.GetItemCount(value, locations) != 0u;

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return hasItem;
                case PrerequisiteComparison.NotEqual:
                    return !hasItem;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ItemOnCharacter}!");
                    return false;
            }
        }
    }
}
