using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the item in objectId is unlocked in the account's costume wardrobe.
    /// </summary>
    /// <remarks>
    /// See 0x1404A17E0 in the client, every condition in the data is 'not already unlocked'.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.CostumeItemUnlocked)]
    public class PrerequisiteCheckCostumeItemUnlocked : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckCostumeItemUnlocked> log;

        public PrerequisiteCheckCostumeItemUnlocked(
            ILogger<PrerequisiteCheckCostumeItemUnlocked> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // only players and ghosts pass
            if (subject is not IPlayer player || subject.Type is not EntityType.Player and not EntityType.Ghost)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, player.Account.CostumeManager.HasItemUnlock(objectId), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.CostumeItemUnlocked}!");

            return result;
        }
    }
}
