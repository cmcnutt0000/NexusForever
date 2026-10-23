using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the player's part in the path holdout in objectId.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// TODO: NexusForever has no holdouts. It fails as the client does.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PathHoldoutPlayer)]
    public class PrerequisiteCheckPathHoldoutPlayer : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPathHoldoutPlayer> log;

        public PrerequisiteCheckPathHoldoutPlayer(
            ILogger<PrerequisiteCheckPathHoldoutPlayer> log)
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
