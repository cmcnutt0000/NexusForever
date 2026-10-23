using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the path mission in value is complete.
    /// </summary>
    /// <remarks>
    /// See 0x14049FDC0 in the client.
    /// TODO: NexusForever doesn't track path missions or episodes.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PlayerPathMission)]
    public class PrerequisiteCheckPlayerPathMission : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPlayerPathMission> log;

        public PrerequisiteCheckPlayerPathMission(
            ILogger<PrerequisiteCheckPlayerPathMission> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PlayerPathMission}!");

            return result;
        }
    }
}
