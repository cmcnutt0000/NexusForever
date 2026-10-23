using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a facility in objectId is within value of the unit.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// TODO: the facility ids aren't identified ("Requires a nearby active teleportation field generator"). It fails as the client does.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.FacilityWithinDistance)]
    public class PrerequisiteCheckFacilityWithinDistance : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckFacilityWithinDistance> log;

        public PrerequisiteCheckFacilityWithinDistance(
            ILogger<PrerequisiteCheckFacilityWithinDistance> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            return false;
        }
    }
}
