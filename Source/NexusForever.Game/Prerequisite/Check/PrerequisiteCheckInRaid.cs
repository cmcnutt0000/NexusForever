using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Group;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player's group is a raid.
    /// </summary>
    /// <remarks>
    /// See 0x14049E9A0 in the client (<see cref="GroupFlags.Raid"/>).
    /// TODO: the world server only knows the player's group id, member count, leader and flags live on the group server. No group counts as a raid.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.InRaid)]
    public class PrerequisiteCheckInRaid : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckInRaid> log;

        public PrerequisiteCheckInRaid(
            ILogger<PrerequisiteCheckInRaid> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.InRaid}!");

            return result;
        }
    }
}
