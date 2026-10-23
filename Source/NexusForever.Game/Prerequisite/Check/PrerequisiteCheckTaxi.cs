using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player is riding a taxi, optionally of the creature in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x14049CDD0 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Taxi)]
    public class PrerequisiteCheckTaxi : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckTaxi> log;

        public PrerequisiteCheckTaxi(
            ILogger<PrerequisiteCheckTaxi> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IWorldEntity platform = subject.PlatformGuid is uint platformGuid ? subject.GetVisible<IWorldEntity>(platformGuid) : null;
            if (platform == null || platform.Type != EntityType.Taxi || platform.CreatureId == 0u)
                return comparison == PrerequisiteComparison.NotEqual;

            if (!PrerequisiteComparer.TryEvaluate(comparison, objectId == 0u || platform.CreatureId == objectId, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Taxi}!");

            return result;
        }
    }
}
