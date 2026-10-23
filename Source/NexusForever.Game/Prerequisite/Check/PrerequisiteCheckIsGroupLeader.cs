using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player leads their group.
    /// </summary>
    /// <remarks>
    /// See 0x14049E850 in the client, which also passes when the player isn't grouped (no leader and no player both compare as 0).
    /// TODO: the world server only knows the player's group id, member count, leader and flags live on the group server. A grouped player isn't treated as leader.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.IsGroupLeader)]
    public class PrerequisiteCheckIsGroupLeader : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckIsGroupLeader> log;

        public PrerequisiteCheckIsGroupLeader(
            ILogger<PrerequisiteCheckIsGroupLeader> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, subject is not IPlayer player || player.GroupAssociation == 0ul, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.IsGroupLeader}!");

            return result;
        }
    }
}
