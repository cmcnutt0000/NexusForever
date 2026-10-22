using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the quest objective with index value can currently be progressed, objectId is the quest.
    /// </summary>
    /// <remarks>
    /// See 0x14049F3D0 in the client, the quest has to be in the quest log and the objective must be incomplete and active.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.IsObjectiveActive)]
    public class PrerequisiteCheckIsObjectiveActive : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckIsObjectiveActive> log;

        public PrerequisiteCheckIsObjectiveActive(
            ILogger<PrerequisiteCheckIsObjectiveActive> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            bool isActive = IsObjectiveActive(player, (ushort)objectId, value);
            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isActive;
                case PrerequisiteComparison.NotEqual:
                    return !isActive;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.IsObjectiveActive}!");
                    return false;
            }
        }

        private static bool IsObjectiveActive(IPlayer player, ushort questId, uint index)
        {
            if (index > byte.MaxValue)
                return false;

            IQuest quest = player.QuestManager.GetActiveQuest(questId);
            IQuestObjective objective = quest?.GetQuestObjectiveByIndex((byte)index);
            if (objective == null || objective.IsComplete())
                return false;

            return quest.IsObjectiveActive((byte)index);
        }
    }
}
