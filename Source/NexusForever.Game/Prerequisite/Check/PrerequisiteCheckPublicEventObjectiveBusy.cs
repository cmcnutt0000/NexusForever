using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.PublicEvent;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the busy state of the public event objective in objectId against the mask in value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0BF0 in the client (objective vtable +0xA0, same as Lua PublicEventObjective:IsBusy).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PublicEventObjectiveBusy)]
    public class PrerequisiteCheckPublicEventObjectiveBusy : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPublicEventObjectiveBusy> log;

        public PrerequisiteCheckPublicEventObjectiveBusy(
            ILogger<PrerequisiteCheckPublicEventObjectiveBusy> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IPublicEventObjective objective = subject.Map?.PublicEventManager.GetActiveObjective(objectId);
            bool isBusy = objective != null && ((objective.IsBusy ? 1u : 0u) & value) != 0u;

            if (!PrerequisiteComparer.TryEvaluate(comparison, isBusy, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PublicEventObjectiveBusy}!");

            return result;
        }
    }
}
