using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Quest;

namespace NexusForever.Game.Prerequisite.Check
{
    [PrerequisiteCheck(PrerequisiteType.QuestState)]
    public class PrerequisiteCheckQuestState : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckQuestState> log;

        public PrerequisiteCheckQuestState(
            ILogger<PrerequisiteCheckQuestState> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            QuestState state = GetQuestState(player, (ushort)objectId);

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return state == (QuestState)value;
                case PrerequisiteComparison.NotEqual:
                    return state != (QuestState)value;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.QuestState}!");
                    return false;
            }
        }

        /// <summary>
        /// Return the <see cref="QuestState"/> the client uses for prerequisites, see 0x14049F690 in the client.
        /// </summary>
        /// <remarks>
        /// A quest that was never in the log is <see cref="QuestState.Unknown"/>, as are quests that are <see cref="QuestState.Botched"/>, <see cref="QuestState.Mentioned"/>, <see cref="QuestState.Abandoned"/> or <see cref="QuestState.Ignored"/>.
        /// </remarks>
        private static QuestState GetQuestState(IPlayer player, ushort questId)
        {
            QuestState? state = player.QuestManager.GetQuestState(questId);
            if (state is null or >= QuestState.Botched and <= QuestState.Ignored)
                return QuestState.Unknown;

            return state.Value;
        }
    }
}
