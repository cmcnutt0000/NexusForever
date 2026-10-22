using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's shield with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 216) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Shield216)]
    public class PrerequisiteCheckShield : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckShield> log;

        public PrerequisiteCheckShield(
            ILogger<PrerequisiteCheckShield> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, subject.Shield, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Shield216}!");

            return result;
        }
    }
}
