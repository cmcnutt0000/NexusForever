using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the checklist entry of the non-player unit is complete in the quest objective with index value, objectId is the quest.
    /// </summary>
    /// <remarks>
    /// See 0x14049F5B0 in the client, the entry is the unit's QuestChecklistIdx and only checklist objectives have entries.
    /// When both units are players NotEqual passes.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.QuestObjective6)]
    public class PrerequisiteCheckQuestObjective6 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckQuestObjective6> log;

        public PrerequisiteCheckQuestObjective6(
            ILogger<PrerequisiteCheckQuestObjective6> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IPlayer player;
            IUnitEntity checklistUnit;
            if (subject is IPlayer subjectPlayer)
            {
                if (parameters.Target == null || parameters.Target is IPlayer)
                    return comparison == PrerequisiteComparison.NotEqual;

                player        = subjectPlayer;
                checklistUnit = parameters.Target;
            }
            else
            {
                player        = parameters.Target as IPlayer;
                checklistUnit = subject;
            }

            bool isComplete = player != null && IsChecklistEntryComplete(player, (ushort)objectId, value, checklistUnit.QuestChecklistIdx);
            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isComplete;
                case PrerequisiteComparison.NotEqual:
                    return !isComplete;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.QuestObjective6}!");
                    return false;
            }
        }

        private static bool IsChecklistEntryComplete(IPlayer player, ushort questId, uint index, byte checklistIdx)
        {
            if (index > byte.MaxValue || checklistIdx >= 32)
                return false;

            IQuestObjective objective = player.QuestManager.GetActiveQuest(questId)?.GetQuestObjectiveByIndex((byte)index);
            if (objective == null || !objective.ObjectiveInfo.IsChecklist())
                return false;

            return (objective.Progress & (1u << checklistIdx)) != 0u;
        }
    }
}
