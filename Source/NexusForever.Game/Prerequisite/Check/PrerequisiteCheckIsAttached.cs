using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit, or the vehicle, mount or taxi it is on, is attached to something.
    /// </summary>
    /// <remarks>
    /// See 0x14049E730 in the client (unit+0xF00).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.IsAttached)]
    public class PrerequisiteCheckIsAttached : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckIsAttached> log;

        public PrerequisiteCheckIsAttached(
            ILogger<PrerequisiteCheckIsAttached> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IWorldEntity entity = subject;
            IWorldEntity platform = subject.PlatformGuid is uint platformGuid ? subject.GetVisible<IWorldEntity>(platformGuid) : null;
            if (platform?.Type is EntityType.Vehicle or EntityType.Mount or EntityType.Taxi)
                entity = platform;

            if (!PrerequisiteComparer.TryEvaluate(comparison, entity.PlatformGuid != null, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.IsAttached}!");

            return result;
        }
    }
}
