using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.PublicEvent;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.PublicEvent;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the completion percentage of the public event objective in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0A70 in the client, count over the required count (the dynamic maximum for dynamic objectives).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PublicEventObjectiveCompletion)]
    public class PrerequisiteCheckPublicEventObjectiveCompletion : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPublicEventObjectiveCompletion> log;

        public PrerequisiteCheckPublicEventObjectiveCompletion(
            ILogger<PrerequisiteCheckPublicEventObjectiveCompletion> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, GetPercent(subject.Map?.PublicEventManager.GetActiveObjective(objectId)), (float)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PublicEventObjectiveCompletion}!");

            return result;
        }

        private static float GetPercent(IPublicEventObjective objective)
        {
            if (objective == null)
                return 0f;

            uint required = (objective.Entry.PublicEventObjectiveFlags & PublicEventObjectiveFlag.DynamicObjective) != 0
                ? objective.DynamicMax
                : objective.Entry.Count;
            return required != 0u ? objective.Count * 100f / required : 0f;
        }
    }
}
