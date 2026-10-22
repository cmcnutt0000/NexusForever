using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's health with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 80) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HealthRequirement)]
    public class PrerequisiteCheckHealthRequirement : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHealthRequirement> log;

        public PrerequisiteCheckHealthRequirement(
            ILogger<PrerequisiteCheckHealthRequirement> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, subject.Health, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.HealthRequirement}!");

            return result;
        }
    }
}
