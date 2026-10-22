using System.Numerics;
using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Quest;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the progress of a quest objective with value, objectId is the quest and the prerequisite type selects the objective index.
    /// </summary>
    /// <remarks>
    /// See 0x14049F4D0 in the client, types 22, 23, 33, 34, 35 and 36 are objective indexes 0 to 5.
    /// </remarks>
    public abstract class PrerequisiteCheckQuestObjectiveProgress : IPrerequisiteCheck
    {
        protected abstract byte ObjectiveIndex { get; }

        #region Dependency Injection

        private readonly ILogger log;
        private readonly IGlobalQuestManager globalQuestManager;

        protected PrerequisiteCheckQuestObjectiveProgress(
            ILogger log,
            IGlobalQuestManager globalQuestManager)
        {
            this.log                = log;
            this.globalQuestManager = globalQuestManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            uint progress = GetProgress(player, (ushort)objectId);
            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return progress == value;
                case PrerequisiteComparison.NotEqual:
                    return progress != value;
                case PrerequisiteComparison.GreaterThan:
                    return progress > value;
                case PrerequisiteComparison.GreaterThanOrEqual:
                    return progress >= value;
                case PrerequisiteComparison.LessThan:
                    return progress < value;
                case PrerequisiteComparison.LessThanOrEqual:
                    return progress <= value;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for quest objective {ObjectiveIndex}!");
                    return false;
            }
        }

        /// <summary>
        /// Return the progress the client shows for the objective, dynamic objectives are shown as a percentage.
        /// </summary>
        private uint GetProgress(IPlayer player, ushort questId)
        {
            if (player.QuestManager.GetQuestState(questId) == QuestState.Completed)
            {
                IQuestInfo info = globalQuestManager.GetQuestInfo(questId);
                if (info == null || ObjectiveIndex >= info.Objectives.Count)
                    return 0u;

                IQuestObjectiveInfo objectiveInfo = info.Objectives[ObjectiveIndex];
                return objectiveInfo.IsDynamic() ? 100u : objectiveInfo.Entry.Count;
            }

            IQuestObjective objective = player.QuestManager.GetActiveQuest(questId)?.GetQuestObjectiveByIndex(ObjectiveIndex);
            if (objective == null)
                return 0u;

            if (objective.ObjectiveInfo.IsChecklist())
                return (uint)BitOperations.PopCount(objective.Progress);

            return objective.ObjectiveInfo.IsDynamic() ? objective.Progress / 10u : objective.Progress;
        }
    }
}
