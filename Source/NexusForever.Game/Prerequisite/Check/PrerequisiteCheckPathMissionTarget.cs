using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the target is one of the units of the path mission in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x14049FFF0 in the client.
    /// TODO: NexusForever doesn't track path missions or episodes.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PathMissionTarget)]
    public class PrerequisiteCheckPathMissionTarget : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPathMissionTarget> log;

        public PrerequisiteCheckPathMissionTarget(
            ILogger<PrerequisiteCheckPathMissionTarget> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PathMissionTarget}!");

            return result;
        }
    }
}
