using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's <see cref="Property"/> in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 55) in the client (unit+0x5C4).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.UnitProperty)]
    public class PrerequisiteCheckUnitProperty : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnitProperty> log;

        public PrerequisiteCheckUnitProperty(
            ILogger<PrerequisiteCheckUnitProperty> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            float propertyValue = Enum.IsDefined((Property)objectId) ? subject.GetPropertyValue((Property)objectId) : 0f;

            if (!PrerequisiteComparer.TryCompare(comparison, propertyValue, (float)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.UnitProperty}!");

            return result;
        }
    }
}
