using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the public event in objectId is in progress.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0B70 in the client (state 1).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PublicEventInProgress)]
    public class PrerequisiteCheckPublicEventInProgress : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPublicEventInProgress> log;

        public PrerequisiteCheckPublicEventInProgress(
            ILogger<PrerequisiteCheckPublicEventInProgress> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, subject.Map?.PublicEventManager.GetActiveEvent(objectId) != null, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PublicEventInProgress}!");

            return result;
        }
    }
}
