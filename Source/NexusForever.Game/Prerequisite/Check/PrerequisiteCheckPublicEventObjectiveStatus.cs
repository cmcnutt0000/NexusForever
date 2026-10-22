using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.PublicEvent;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.PublicEvent;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the <see cref="PublicEventStatus"/> of the public event objective in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0590 in the client, the status is Inactive when the objective's public event isn't active.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PublicEventObjectiveStatus)]
    public class PrerequisiteCheckPublicEventObjectiveStatus : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPublicEventObjectiveStatus> log;

        public PrerequisiteCheckPublicEventObjectiveStatus(
            ILogger<PrerequisiteCheckPublicEventObjectiveStatus> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IPublicEventObjective objective = subject.Map?.PublicEventManager.GetActiveObjective(objectId);
            PublicEventStatus status = objective?.Status ?? PublicEventStatus.Inactive;

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return status == (PublicEventStatus)value;
                case PrerequisiteComparison.NotEqual:
                    return status != (PublicEventStatus)value;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PublicEventObjectiveStatus}!");
                    return false;
            }
        }
    }
}
