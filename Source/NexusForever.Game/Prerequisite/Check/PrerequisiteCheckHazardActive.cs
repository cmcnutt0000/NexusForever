using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the hazard in objectId is active.
    /// </summary>
    /// <remarks>
    /// See 0x1404A03E0 in the client, an unknown hazard fails <see cref="PrerequisiteComparison.Equal"/>.
    /// TODO: NexusForever has no hazards.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HazardActive)]
    public class PrerequisiteCheckHazardActive : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHazardActive> log;

        public PrerequisiteCheckHazardActive(
            ILogger<PrerequisiteCheckHazardActive> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.HazardActive}!");

            return result;
        }
    }
}
