using NexusForever.Game.Static.Quest;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Abstract.Quest
{
    public interface IQuestObjectiveInfo
    {
        uint Id { get; }
        QuestObjectiveType Type { get; }

        QuestObjectiveEntry Entry { get; }

        /// <summary>
        /// Quest objective is sequential, previous required quest objectives must be complete.
        /// </summary>
        bool IsSequential();

        /// <summary>
        /// Quest objective follows previous objectives, it is only active while the previous quest objectives are complete or active.
        /// </summary>
        bool FollowsPrevious();

        /// <summary>
        /// Quest objective is never shown in the objective list.
        /// </summary>
        bool IsHidden();

        /// <summary>
        /// Quest objective is optional, doesn't need to be complete for quest to be achieved.
        /// </summary>
        bool IsOptional();

        /// <summary>
        /// Quest objective progress is a count rather than a percentage.
        /// </summary>
        bool IsProgressCounted();

        /// <summary>
        /// Quest objective progress is a percentage in tenths (0-1000) rather than a count.
        /// </summary>
        bool IsDynamic();
    }
}