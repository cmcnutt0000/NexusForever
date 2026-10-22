using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Quest;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Quest
{
    public class QuestObjectiveInfo : IQuestObjectiveInfo
    {
        public uint Id => Entry.Id;
        public QuestObjectiveType Type => (QuestObjectiveType)Entry.Type;

        public QuestObjectiveEntry Entry { get; }

        /// <summary>
        /// Create a new <see cref="QuestObjectiveInfo"/> with supplied <see cref="QuestObjectiveEntry"/>.
        /// </summary>
        public QuestObjectiveInfo(QuestObjectiveEntry entry)
        {
            Entry = entry;
        }

        /// <summary>
        /// Quest objective is sequential, previous required quest objectives must be complete.
        /// </summary>
        public bool IsSequential()
        {
            return ((QuestObjectiveFlags)Entry.Flags & QuestObjectiveFlags.Sequential) != 0;
        }

        /// <summary>
        /// Quest objective follows previous objectives, it is only active while the previous quest objectives are complete or active.
        /// </summary>
        public bool FollowsPrevious()
        {
            return ((QuestObjectiveFlags)Entry.Flags & QuestObjectiveFlags.FollowsPrevious) != 0;
        }

        /// <summary>
        /// Quest objective is never shown in the objective list.
        /// </summary>
        public bool IsHidden()
        {
            return ((QuestObjectiveFlags)Entry.Flags & QuestObjectiveFlags.Hidden) != 0;
        }

        /// <summary>
        /// Quest objective is optional, doesn't need to be complete for quest to be achieved.
        /// </summary>
        public bool IsOptional()
        {
            return ((QuestObjectiveFlags)Entry.Flags & QuestObjectiveFlags.Optional) != 0;
        }

        /// <summary>
        /// Quest objective progress is a count rather than a percentage.
        /// </summary>
        public bool IsProgressCounted()
        {
            return ((QuestObjectiveFlags)Entry.Flags & QuestObjectiveFlags.CountedProgress) != 0;
        }

        /// <summary>
        /// Quest objective progress is a percentage in tenths (0-1000) rather than a count.
        /// </summary>
        /// <remarks>
        /// Kill objectives with a count above 1 are dynamic unless <see cref="QuestObjectiveFlags.CountedProgress"/> is set, see 0x140551840 in the client.
        /// </remarks>
        public bool IsDynamic()
        {
            return Type is QuestObjectiveType.KillCreature
                    or QuestObjectiveType.KillTargetGroups
                    or QuestObjectiveType.KillCluster
                    or QuestObjectiveType.KillTargetGroup
                    or QuestObjectiveType.KillCreature2
                && Entry.Count > 1u
                && !IsProgressCounted();
        }
    }
}
