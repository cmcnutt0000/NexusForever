using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the in game hour with value.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// Inferred from the data: the values are 0 to 23. The clock is the one sent in ServerTimeOfDay.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.TimeOfDay)]
    public class PrerequisiteCheckTimeOfDay : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckTimeOfDay> log;

        public PrerequisiteCheckTimeOfDay(
            ILogger<PrerequisiteCheckTimeOfDay> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, (uint)InGameTime.TimeOfDay.Hours, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.TimeOfDay}!");

            return result;
        }
    }
}
