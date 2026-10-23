using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player has the item in value equipped.
    /// </summary>
    /// <remarks>
    /// See 0x14049D1C0 in the client, mask 0x001.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ItemEquipped)]
    public class PrerequisiteCheckItemEquipped : IPrerequisiteCheck
    {
        private static readonly InventoryLocation[] locations =
        {
            InventoryLocation.Equipped
        };

        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItemEquipped> log;

        public PrerequisiteCheckItemEquipped(
            ILogger<PrerequisiteCheckItemEquipped> log)
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
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ItemEquipped}!");
                    return false;
            }
        }
    }
}
