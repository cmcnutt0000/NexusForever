using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Quest;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Quest
{
    public class QuestObjectiveInfo : IQuestObjectiveInfo
    {
        public uint Id => Entry.Id;
        public QuestObjectiveType Type => (QuestObjectiveType)Entry.Type;

        public QuestObjectiveEntry Entry { get; }

        private readonly bool isChecklist;

        /// <summary>
        /// Create a new <see cref="QuestObjectiveInfo"/> with supplied <see cref="QuestObjectiveEntry"/>.
        /// </summary>
        public QuestObjectiveInfo(QuestObjectiveEntry entry)
        {
            Entry       = entry;
            isChecklist = IsChecklistEntry(entry);
        }

        private static bool IsChecklistEntry(QuestObjectiveEntry entry)
        {
            switch ((QuestObjectiveType)entry.Type)
            {
                case QuestObjectiveType.Checklist:
                case QuestObjectiveType.SpellSuccess2:
                case QuestObjectiveType.ActivateTargetGroupChecklist:
                case QuestObjectiveType.TalkToTargetGroup:
                    return true;
                case QuestObjectiveType.Unknown27:
                case QuestObjectiveType.Unknown28:
                {
                    PathSoldierActivateEntry activateEntry = GameTableManager.Instance.PathSoldierActivate.GetEntry(entry.Data);
                    return activateEntry?.SoldierActivateModeEnum is 1u or 2u;
                }
                default:
                    return false;
            }
        }

        /// <summary>
        /// Quest objective progress is a bitmask of completed checklist entries rather than a count.
        /// </summary>
        /// <remarks>
        /// Each entry is the QuestChecklistIdx of an entity, the objective is complete once the number of set bits reaches the count, see 0x1405517A0 in the client.
        /// </remarks>
        public bool IsChecklist()
        {
            return isChecklist;
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
