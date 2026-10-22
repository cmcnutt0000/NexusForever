using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    [PrerequisiteCheck(PrerequisiteType.QuestObjective4)]
    public class PrerequisiteCheckQuestObjective4 : PrerequisiteCheckQuestObjectiveProgress
    {
        protected override byte ObjectiveIndex => 4;

        public PrerequisiteCheckQuestObjective4(
            ILogger<PrerequisiteCheckQuestObjective4> log,
            IGlobalQuestManager globalQuestManager)
            : base(log, globalQuestManager)
        {
        }
    }
}
