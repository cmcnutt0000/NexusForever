using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares a roll from 1 to 100 with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 37) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.RandomPercent)]
    public class PrerequisiteCheckRandomPercent : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckRandomPercent> log;

        public PrerequisiteCheckRandomPercent(
            ILogger<PrerequisiteCheckRandomPercent> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            uint roll = (uint)Random.Shared.Next(1, 101);

            if (!PrerequisiteComparer.TryCompare(comparison, roll, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.RandomPercent}!");

            return result;
        }
    }
}
