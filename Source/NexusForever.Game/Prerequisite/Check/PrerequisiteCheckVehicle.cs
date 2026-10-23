using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is riding a vehicle, optionally of the creature in objectId and in the seat in value.
    /// </summary>
    /// <remarks>
    /// See 0x14049CBB0 in the client (unit+0xC0 and unit+0xC8).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Vehicle)]
    public class PrerequisiteCheckVehicle : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckVehicle> log;

        public PrerequisiteCheckVehicle(
            ILogger<PrerequisiteCheckVehicle> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IWorldEntity platform = subject.PlatformGuid is uint platformGuid ? subject.GetVisible<IWorldEntity>(platformGuid) : null;
            if (platform == null || platform.CreatureId == 0u)
                return comparison == PrerequisiteComparison.NotEqual;

            // objectId 0 is any creature and value 0xFFFFFFFF any seat
            bool isRiding = (objectId == 0u || platform.CreatureId == objectId)
                && (value == uint.MaxValue || (platform as IVehicleEntity)?.GetPassenger(subject.Guid)?.SeatPosition == value);

            if (!PrerequisiteComparer.TryEvaluate(comparison, isRiding, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Vehicle}!");

            return result;
        }
    }
}
