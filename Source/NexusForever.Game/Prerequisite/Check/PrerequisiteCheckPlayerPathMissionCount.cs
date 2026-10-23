using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the progress count of the path mission in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A00E0 in the client.
    /// TODO: NexusForever doesn't track path missions or episodes.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PlayerPathMissionCount)]
    public class PrerequisiteCheckPlayerPathMissionCount : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPlayerPathMissionCount> log;

        public PrerequisiteCheckPlayerPathMissionCount(
            ILogger<PrerequisiteCheckPlayerPathMissionCount> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PlayerPathMissionCount}!");

            return result;
        }
    }
}
