using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the public event in objectId is active.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0540 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PublicEvent117)]
    public class PrerequisiteCheckPublicEvent117 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPublicEvent117> log;

        public PrerequisiteCheckPublicEvent117(
            ILogger<PrerequisiteCheckPublicEvent117> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isActive = subject.Map?.PublicEventManager.GetActiveEvent(objectId) != null;

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isActive;
                case PrerequisiteComparison.NotEqual:
                    return !isActive;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PublicEvent117}!");
                    return false;
            }
        }
    }
}
