using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.PublicEvent;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit has been counted by the public event objective in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0650 in the client, which tests the objective's checklist bit for the unit's checklist index.
    /// Without a non-player unit only <see cref="PrerequisiteComparison.NotEqual"/> passes.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PublicEventObjectiveObject)]
    public class PrerequisiteCheckPublicEventObjectiveObject : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPublicEventObjectiveObject> log;

        public PrerequisiteCheckPublicEventObjectiveObject(
            ILogger<PrerequisiteCheckPublicEventObjectiveObject> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // the non-player unit is the subject, or the target when the subject is a player
            IUnitEntity unit = subject is IPlayer ? parameters?.Target : subject;
            if (unit == null || unit is IPlayer)
                return comparison == PrerequisiteComparison.NotEqual;

            IPublicEventObjective objective = subject.Map?.PublicEventManager.GetActiveObjective(objectId);
            bool isCounted = objective != null && unit.QuestChecklistIdx < 32 && (objective.Checklist & (1u << unit.QuestChecklistIdx)) != 0u;

            if (!PrerequisiteComparer.TryEvaluate(comparison, isCounted, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PublicEventObjectiveObject}!");

            return result;
        }
    }
}
