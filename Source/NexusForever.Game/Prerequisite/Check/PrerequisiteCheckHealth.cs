using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's health as a percentage of its maximum health with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 40) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Health)]
    public class PrerequisiteCheckHealth : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHealth> log;

        public PrerequisiteCheckHealth(
            ILogger<PrerequisiteCheckHealth> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            float percent = subject.MaxHealth != 0u ? subject.Health * 100f / subject.MaxHealth : 0f;

            if (!PrerequisiteComparer.TryCompare(comparison, percent, (float)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Health}!");

            return result;
        }
    }
}
