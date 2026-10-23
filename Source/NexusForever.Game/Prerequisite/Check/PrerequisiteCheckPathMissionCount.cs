using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the number of completed missions of the path episode in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049FEF0 in the client.
    /// TODO: NexusForever doesn't track path missions or episodes.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PathMissionCount)]
    public class PrerequisiteCheckPathMissionCount : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPathMissionCount> log;

        public PrerequisiteCheckPathMissionCount(
            ILogger<PrerequisiteCheckPathMissionCount> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PathMissionCount}!");

            return result;
        }
    }
}
