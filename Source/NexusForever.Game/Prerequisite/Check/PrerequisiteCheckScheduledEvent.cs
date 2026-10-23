using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the scheduled event in value is active.
    /// </summary>
    /// <remarks>
    /// See 0x14049EF00 in the client.
    /// TODO: NexusForever doesn't track scheduled events.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ScheduledEvent)]
    public class PrerequisiteCheckScheduledEvent : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckScheduledEvent> log;

        public PrerequisiteCheckScheduledEvent(
            ILogger<PrerequisiteCheckScheduledEvent> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ScheduledEvent}!");

            return result;
        }
    }
}
