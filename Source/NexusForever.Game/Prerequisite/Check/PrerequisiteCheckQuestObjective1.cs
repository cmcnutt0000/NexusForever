using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    [PrerequisiteCheck(PrerequisiteType.QuestObjective1)]
    public class PrerequisiteCheckQuestObjective1 : PrerequisiteCheckQuestObjectiveProgress
    {
        protected override byte ObjectiveIndex => 1;

        public PrerequisiteCheckQuestObjective1(
            ILogger<PrerequisiteCheckQuestObjective1> log,
            IGlobalQuestManager globalQuestManager)
            : base(log, globalQuestManager)
        {
        }
    }
}
