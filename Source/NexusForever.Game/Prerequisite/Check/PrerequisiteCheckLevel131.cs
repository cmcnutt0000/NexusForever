using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's level with the target's level, value isn't used.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 131) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Level131)]
    public class PrerequisiteCheckLevel131 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckLevel131> log;

        public PrerequisiteCheckLevel131(
            ILogger<PrerequisiteCheckLevel131> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (parameters.Target == null)
                return comparison == PrerequisiteComparison.NotEqual;

            if (!PrerequisiteComparer.TryCompare(comparison, subject.Level, parameters.Target.Level, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Level131}!");

            return result;
        }
    }
}
