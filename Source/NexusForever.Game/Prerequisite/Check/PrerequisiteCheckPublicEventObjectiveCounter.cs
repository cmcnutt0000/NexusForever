using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the sum of the counters for objectId of the active public events with value.
    /// </summary>
    /// <remarks>
    /// See 0x1405F68F0 in the client, which sums objective type 28 counters; only players and ghosts pass.
    /// TODO: NexusForever has no objective type 28, so the sum is 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PublicEventObjectiveCounter)]
    public class PrerequisiteCheckPublicEventObjectiveCounter : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPublicEventObjectiveCounter> log;

        public PrerequisiteCheckPublicEventObjectiveCounter(
            ILogger<PrerequisiteCheckPublicEventObjectiveCounter> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject.Type is not EntityType.Player and not EntityType.Ghost)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PublicEventObjectiveCounter}!");

            return result;
        }
    }
}
