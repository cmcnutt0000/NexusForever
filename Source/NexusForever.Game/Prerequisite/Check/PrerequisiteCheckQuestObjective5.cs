using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    [PrerequisiteCheck(PrerequisiteType.QuestObjective5)]
    public class PrerequisiteCheckQuestObjective5 : PrerequisiteCheckQuestObjectiveProgress
    {
        protected override byte ObjectiveIndex => 5;

        public PrerequisiteCheckQuestObjective5(
            ILogger<PrerequisiteCheckQuestObjective5> log,
            IGlobalQuestManager globalQuestManager)
            : base(log, globalQuestManager)
        {
        }
    }
}
