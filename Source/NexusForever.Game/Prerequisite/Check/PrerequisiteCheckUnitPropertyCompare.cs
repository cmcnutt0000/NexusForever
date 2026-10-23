using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's <see cref="Property"/> in objectId with its <see cref="Property"/> in value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 115) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.UnitPropertyCompare)]
    public class PrerequisiteCheckUnitPropertyCompare : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnitPropertyCompare> log;

        public PrerequisiteCheckUnitPropertyCompare(
            ILogger<PrerequisiteCheckUnitPropertyCompare> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, GetPropertyValue(subject, objectId), GetPropertyValue(subject, value), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.UnitPropertyCompare}!");

            return result;
        }

        private static float GetPropertyValue(IUnitEntity unit, uint property)
        {
            return Enum.IsDefined((Property)property) ? unit.GetPropertyValue((Property)property) : 0f;
        }
    }
}
