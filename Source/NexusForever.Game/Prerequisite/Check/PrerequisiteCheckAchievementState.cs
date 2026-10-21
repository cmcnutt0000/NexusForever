using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    [PrerequisiteCheck(PrerequisiteType.AchievementState)]
    public class PrerequisiteCheckAchievementState : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckAchievementState> log;

        public PrerequisiteCheckAchievementState(
            ILogger<PrerequisiteCheckAchievementState> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            // the client compares the completion state (1 completed, 0 not completed) with value, see 0x14049F770 in the client
            // value 0 is used to require that an achievement is not completed yet
            uint state = player.AchievementManager.HasCompletedAchievement((ushort)objectId) ? 1u : 0u;

            switch (comparison)
            {
                case PrerequisiteComparison.NotEqual:
                    return state != value;
                case PrerequisiteComparison.Equal:
                    return state == value;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.AchievementState}!");
                    return false;
            }
        }
    }
}
