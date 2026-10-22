using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.PublicEvent;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the public event in objectId is active and, unless value is 0, in the phase in value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0760 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PublicEvent120)]
    public class PrerequisiteCheckPublicEvent120 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPublicEvent120> log;

        public PrerequisiteCheckPublicEvent120(
            ILogger<PrerequisiteCheckPublicEvent120> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IPublicEvent publicEvent = subject.Map?.PublicEventManager.GetActiveEvent(objectId);
            bool matches = publicEvent != null && (value == 0u || publicEvent.Phase == value);

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return matches;
                case PrerequisiteComparison.NotEqual:
                    return !matches;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PublicEvent120}!");
                    return false;
            }
        }
    }
}
