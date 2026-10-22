using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's shield as a percentage of its maximum shield with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 215) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Shield215)]
    public class PrerequisiteCheckShieldPercent : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckShieldPercent> log;

        public PrerequisiteCheckShieldPercent(
            ILogger<PrerequisiteCheckShieldPercent> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            float percent = subject.MaxShieldCapacity != 0u ? subject.Shield * 100f / subject.MaxShieldCapacity : 0f;

            if (!PrerequisiteComparer.TryCompare(comparison, percent, (float)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Shield215}!");

            return result;
        }
    }
}
