using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Quest;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the quest objective with index value is complete, objectId is the quest.
    /// </summary>
    /// <remarks>
    /// See 0x14049F460 and 0x1405510D0 in the client, every objective of a completed quest is complete.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.QuestObjective)]
    public class PrerequisiteCheckQuestObjective : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckQuestObjective> log;
        private readonly IGlobalQuestManager globalQuestManager;

        public PrerequisiteCheckQuestObjective(
            ILogger<PrerequisiteCheckQuestObjective> log,
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

            bool isComplete = IsObjectiveComplete(player, (ushort)objectId, value);
            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isComplete;
                case PrerequisiteComparison.NotEqual:
                    return !isComplete;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.QuestObjective}!");
                    return false;
            }
        }

        private bool IsObjectiveComplete(IPlayer player, ushort questId, uint index)
        {
            if (player.QuestManager.GetQuestState(questId) == QuestState.Completed)
            {
                IQuestInfo info = globalQuestManager.GetQuestInfo(questId);
                return info != null && index < info.Objectives.Count;
            }

            if (index > byte.MaxValue)
                return false;

            IQuestObjective objective = player.QuestManager.GetActiveQuest(questId)?.GetQuestObjectiveByIndex((byte)index);
            return objective?.IsComplete() ?? false;
        }
    }
}
