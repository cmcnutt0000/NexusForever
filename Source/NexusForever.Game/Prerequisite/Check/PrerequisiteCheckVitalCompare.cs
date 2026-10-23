using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's <see cref="Vital"/> in objectId with its <see cref="Vital"/> in value.
    /// </summary>
    /// <remarks>
    /// See 0x140466520 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.VitalCompare)]
    public class PrerequisiteCheckVitalCompare : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckVitalCompare> log;

        public PrerequisiteCheckVitalCompare(
            ILogger<PrerequisiteCheckVitalCompare> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, subject.GetVital((Vital)objectId), subject.GetVital((Vital)value), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.VitalCompare}!");

            return result;
        }
    }
}
