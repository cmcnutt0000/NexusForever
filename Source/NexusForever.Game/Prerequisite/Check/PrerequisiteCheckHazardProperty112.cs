using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks a property of the hazard in objectId against value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0490 in the client.
    /// TODO: NexusForever has no hazards.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HazardProperty112)]
    public class PrerequisiteCheckHazardProperty112 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHazardProperty112> log;

        public PrerequisiteCheckHazardProperty112(
            ILogger<PrerequisiteCheckHazardProperty112> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.HazardProperty112}!");

            return result;
        }
    }
}
