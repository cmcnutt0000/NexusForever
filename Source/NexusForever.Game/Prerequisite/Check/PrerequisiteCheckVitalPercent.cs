using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's <see cref="Vital"/> in objectId as a percentage of its maximum with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A4E30 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.VitalPercent)]
    public class PrerequisiteCheckVitalPercent : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckVitalPercent> log;

        public PrerequisiteCheckVitalPercent(
            ILogger<PrerequisiteCheckVitalPercent> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            float maximum = subject.GetVitalMax((Vital)objectId);
            float percent = maximum != 0f ? subject.GetVital((Vital)objectId) * 100f / maximum : 0f;

            if (!PrerequisiteComparer.TryCompare(comparison, percent, (float)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.VitalPercent}!");

            return result;
        }
    }
}
