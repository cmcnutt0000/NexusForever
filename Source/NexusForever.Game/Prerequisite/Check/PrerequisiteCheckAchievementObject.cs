using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Achievement;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if checklist item value of the achievement in objectId is complete.
    /// </summary>
    /// <remarks>
    /// See 0x14049F810 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.AchievementObject)]
    public class PrerequisiteCheckAchievementObject : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckAchievementObject> log;

        public PrerequisiteCheckAchievementObject(
            ILogger<PrerequisiteCheckAchievementObject> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            IAchievement achievement = player.AchievementManager.GetAchievementProgress((ushort)objectId);
            bool isComplete = achievement != null && value < 32u && (achievement.Data0 & (1u << (int)value)) != 0u;

            if (!PrerequisiteComparer.TryEvaluate(comparison, isComplete, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.AchievementObject}!");

            return result;
        }
    }
}
