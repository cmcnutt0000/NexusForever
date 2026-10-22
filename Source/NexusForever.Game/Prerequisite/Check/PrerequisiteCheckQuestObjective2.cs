using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    [PrerequisiteCheck(PrerequisiteType.QuestObjective2)]
    public class PrerequisiteCheckQuestObjective2 : PrerequisiteCheckQuestObjectiveProgress
    {
        protected override byte ObjectiveIndex => 2;

        public PrerequisiteCheckQuestObjective2(
            ILogger<PrerequisiteCheckQuestObjective2> log,
            IGlobalQuestManager globalQuestManager)
            : base(log, globalQuestManager)
        {
        }
    }
}
