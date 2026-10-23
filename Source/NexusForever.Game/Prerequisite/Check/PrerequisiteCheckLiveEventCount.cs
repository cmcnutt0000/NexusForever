using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the count of the live event in objectId with value.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// TODO: NexusForever doesn't track live events, the count is 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.LiveEventCount)]
    public class PrerequisiteCheckLiveEventCount : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckLiveEventCount> log;

        public PrerequisiteCheckLiveEventCount(
            ILogger<PrerequisiteCheckLiveEventCount> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.LiveEventCount}!");

            return result;
        }
    }
}
