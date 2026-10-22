using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.PublicEvent;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the count of the public event objective in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A09D0 in the client, the count is 0 when the objective's public event isn't active.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PublicEventObjectiveCount)]
    public class PrerequisiteCheckPublicEventObjectiveCount : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPublicEventObjectiveCount> log;

        public PrerequisiteCheckPublicEventObjectiveCount(
            ILogger<PrerequisiteCheckPublicEventObjectiveCount> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IPublicEventObjective objective = subject.Map?.PublicEventManager.GetActiveObjective(objectId);
            uint count = objective?.Count ?? 0u;

            if (!PrerequisiteComparer.TryCompare(comparison, count, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PublicEventObjectiveCount}!");

            return result;
        }
    }
}
