using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    [PrerequisiteCheck(PrerequisiteType.QuestObjective3)]
    public class PrerequisiteCheckQuestObjective3 : PrerequisiteCheckQuestObjectiveProgress
    {
        protected override byte ObjectiveIndex => 3;

        public PrerequisiteCheckQuestObjective3(
            ILogger<PrerequisiteCheckQuestObjective3> log,
            IGlobalQuestManager globalQuestManager)
            : base(log, globalQuestManager)
        {
        }
    }
}
