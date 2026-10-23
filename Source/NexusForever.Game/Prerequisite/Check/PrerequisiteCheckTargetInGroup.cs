using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the target is in the player's group.
    /// </summary>
    /// <remarks>
    /// See 0x14049E900 in the client, with no target only <see cref="PrerequisiteComparison.NotEqual"/> passes.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.TargetInGroup)]
    public class PrerequisiteCheckTargetInGroup : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckTargetInGroup> log;

        public PrerequisiteCheckTargetInGroup(
            ILogger<PrerequisiteCheckTargetInGroup> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IUnitEntity target = parameters?.Target;
            if (target == null)
                return comparison == PrerequisiteComparison.NotEqual;

            bool isGrouped = subject is IPlayer player && target is IPlayer targetPlayer
                && player.GroupAssociation != 0ul && player.GroupAssociation == targetPlayer.GroupAssociation;

            if (!PrerequisiteComparer.TryEvaluate(comparison, isGrouped, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.TargetInGroup}!");

            return result;
        }
    }
}
