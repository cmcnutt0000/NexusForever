using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the account's loyalty points with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A16C0 in the client, only players and ghosts pass.
    /// TODO: NexusForever doesn't track loyalty points.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.LoyaltyRewards)]
    public class PrerequisiteCheckLoyaltyRewards : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckLoyaltyRewards> log;

        public PrerequisiteCheckLoyaltyRewards(
            ILogger<PrerequisiteCheckLoyaltyRewards> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // only players and ghosts pass
            if (subject.Type is not EntityType.Player and not EntityType.Ghost)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.LoyaltyRewards}!");

            return result;
        }
    }
}
