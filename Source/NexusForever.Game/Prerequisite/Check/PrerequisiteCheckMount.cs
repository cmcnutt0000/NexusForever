using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is riding a mount, optionally of the creature in objectId and in the seat in value.
    /// </summary>
    /// <remarks>
    /// See 0x14049CCC0 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Mount)]
    public class PrerequisiteCheckMount : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckMount> log;

        public PrerequisiteCheckMount(
            ILogger<PrerequisiteCheckMount> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IWorldEntity platform = subject.PlatformGuid is uint platformGuid ? subject.GetVisible<IWorldEntity>(platformGuid) : null;
            if (platform == null || platform.CreatureId == 0u || platform.Type != EntityType.Mount)
                return comparison == PrerequisiteComparison.NotEqual;

            // objectId 0 is any creature and value 0xFFFFFFFF any seat
            bool isRiding = (objectId == 0u || platform.CreatureId == objectId)
                && (value == uint.MaxValue || (platform as IVehicleEntity)?.GetPassenger(subject.Guid)?.SeatPosition == value);

            if (!PrerequisiteComparer.TryEvaluate(comparison, isRiding, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Mount}!");

            return result;
        }
    }
}
