using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Achievement;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the achievement in objectId has the checklist item of the unit complete.
    /// </summary>
    /// <remarks>
    /// See 0x14049F8C0 in the client, the unit is the target when the subject is a player and its checklist index picks the item.
    /// Without a non-player unit only <see cref="PrerequisiteComparison.NotEqual"/> passes.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.AchievementChecklistUnit)]
    public class PrerequisiteCheckAchievementChecklistUnit : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckAchievementChecklistUnit> log;

        public PrerequisiteCheckAchievementChecklistUnit(
            ILogger<PrerequisiteCheckAchievementChecklistUnit> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IUnitEntity unit = subject is IPlayer ? parameters?.Target : subject;
            IPlayer player = subject as IPlayer ?? parameters?.Target as IPlayer;
            if (unit == null || unit is IPlayer || player == null)
                return comparison == PrerequisiteComparison.NotEqual;

            IAchievement achievement = player.AchievementManager.GetAchievementProgress((ushort)objectId);
            bool isComplete = achievement != null && unit.QuestChecklistIdx < 32 && (achievement.Data0 & (1u << unit.QuestChecklistIdx)) != 0u;

            if (!PrerequisiteComparer.TryEvaluate(comparison, isComplete, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.AchievementChecklistUnit}!");

            return result;
        }
    }
}
