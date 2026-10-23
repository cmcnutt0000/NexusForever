using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the value of the hazard in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A04F0 in the client, an unknown hazard is 0.
    /// TODO: NexusForever has no hazards.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HazardValue)]
    public class PrerequisiteCheckHazardValue : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHazardValue> log;

        public PrerequisiteCheckHazardValue(
            ILogger<PrerequisiteCheckHazardValue> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, 0f, (float)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.HazardValue}!");

            return result;
        }
    }
}
