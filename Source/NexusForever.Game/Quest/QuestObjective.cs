using System.Numerics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NexusForever.Database.Character;
using NexusForever.Database.Character.Model;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Quest;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Quest
{
    public class QuestObjective : IQuestObjective
    {
        [Flags]
        public enum QuestObjectiveSaveMask
        {
            None     = 0x00,
            Create   = 0x01,
            Progress = 0x02,
            Timer    = 0x04
        }

        public IQuestInfo QuestInfo { get; }
        public IQuestObjectiveInfo ObjectiveInfo { get; }

        public byte Index { get; }

        public uint Progress
        {
            get => progress;
            set
            {
                saveMask |= QuestObjectiveSaveMask.Progress;
                progress = value;
            }
        }

        private uint progress;

        public uint? Timer
        {
            get => timer;
            set
            {
                saveMask |= QuestObjectiveSaveMask.Timer;
                timer = value;
            }
        }

        private uint? timer;

        private QuestObjectiveSaveMask saveMask;

        private readonly IPlayer player;

        /// <summary>
        /// Create a new <see cref="IQuestObjective"/> from an existing database model.
        /// </summary>
        public QuestObjective(IPlayer owner, IQuestInfo questInfo, IQuestObjectiveInfo objectiveInfo, CharacterQuestObjectiveModel model)
        {
            player        = owner;

            QuestInfo     = questInfo;
            ObjectiveInfo = objectiveInfo;
            Index         = model.Index;
            progress      = model.Progress;
            timer         = model.Timer;
        }

        /// <summary>
        /// Create a new <see cref="IQuestObjective"/> from supplied <see cref="QuestObjectiveEntry"/>.
        /// </summary>
        public QuestObjective(IPlayer owner, IQuestInfo questInfo, IQuestObjectiveInfo objectiveInfo, byte index)
        {
            player        = owner;

            QuestInfo     = questInfo;
            ObjectiveInfo = objectiveInfo;
            Index         = index;

            if (objectiveInfo.Entry.MaxTimeAllowedMS != 0u)
            {
                // TODO
            }

            saveMask = QuestObjectiveSaveMask.Create;
        }

        public void Save(CharacterContext context)
        {
            if (saveMask == QuestObjectiveSaveMask.None)
                return;

            if ((saveMask & QuestObjectiveSaveMask.Create) != 0)
            {
                context.Add(new CharacterQuestObjectiveModel
                {
                    Id       = player.CharacterId,
                    QuestId  = (ushort)QuestInfo.Entry.Id,
                    Index    = Index,
                    Progress = Progress
                });
            }
            else
            {
                var model = new CharacterQuestObjectiveModel
                {
                    Id      = player.CharacterId,
                    QuestId = (ushort)QuestInfo.Entry.Id,
                    Index   = Index
                };

                EntityEntry<CharacterQuestObjectiveModel> entity = context.Entry(model);
                if ((saveMask & QuestObjectiveSaveMask.Progress) != 0)
                {
                    model.Progress = Progress;
                    entity.Property(p => p.Progress).IsModified = true;
                }

                if ((saveMask & QuestObjectiveSaveMask.Timer) != 0)
                {
                    // TODO
                }
            }

            saveMask = QuestObjectiveSaveMask.None;
        }

        public void Update(double lastTick)
        {
            // TODO: update timer
        }

        /// <summary>
        /// Return if the objective has been completed.
        /// </summary>
        public bool IsComplete()
        {
            if (ObjectiveInfo.IsChecklist())
                return BitOperations.PopCount(progress) >= ObjectiveInfo.Entry.Count;

            return progress >= GetMaxValue();
        }

        private uint GetMaxValue()
        {
            return ObjectiveInfo.IsDynamic() ? 1000u : ObjectiveInfo.Entry.Count;
        }

        /// <summary>
        /// Update object progress with supplied update.
        /// </summary>
        /// <remarks>
        /// For checklist objectives the update is the QuestChecklistIdx of the completed entry.
        /// </remarks>
        public void ObjectiveUpdate(uint update)
        {
            if (ObjectiveInfo.IsChecklist())
            {
                if (update < 32u && (progress & (1u << (int)update)) == 0u)
                    Progress = progress | (1u << (int)update);
                return;
            }

            if (ObjectiveInfo.IsDynamic())
                update = (uint)(((float)update / ObjectiveInfo.Entry.Count) * 1000f);

            Progress = Math.Min(progress + update, GetMaxValue());
        }

        public void Complete()
        {
            if (ObjectiveInfo.IsChecklist())
            {
                // any set bits count towards the checklist, fill from the lowest entry
                uint count = ObjectiveInfo.Entry.Count;
                Progress = progress | (count >= 32u ? uint.MaxValue : (1u << (int)count) - 1u);
                return;
            }

            Progress = GetMaxValue();
        }
    }
}
