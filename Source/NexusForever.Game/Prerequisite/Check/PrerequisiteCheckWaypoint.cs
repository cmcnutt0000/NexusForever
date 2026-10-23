using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the waypoint direction.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// TODO: the arguments aren't identified. It fails as the client does.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Waypoint)]
    public class PrerequisiteCheckWaypoint : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckWaypoint> log;

        public PrerequisiteCheckWaypoint(
            ILogger<PrerequisiteCheckWaypoint> log)
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
