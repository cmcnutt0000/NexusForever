using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    [PrerequisiteCheck(PrerequisiteType.QuestObjective0)]
    public class PrerequisiteCheckQuestObjective0 : PrerequisiteCheckQuestObjectiveProgress
    {
        protected override byte ObjectiveIndex => 0;

        public PrerequisiteCheckQuestObjective0(
            ILogger<PrerequisiteCheckQuestObjective0> log,
            IGlobalQuestManager globalQuestManager)
            : base(log, globalQuestManager)
        {
        }
    }
}
