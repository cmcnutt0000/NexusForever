using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's level, ignoring mentoring, with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 177) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.TrueLevel)]
    public class PrerequisiteCheckTrueLevel : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckTrueLevel> log;

        public PrerequisiteCheckTrueLevel(
            ILogger<PrerequisiteCheckTrueLevel> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, subject.Level, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.TrueLevel}!");

            return result;
        }
    }
}
