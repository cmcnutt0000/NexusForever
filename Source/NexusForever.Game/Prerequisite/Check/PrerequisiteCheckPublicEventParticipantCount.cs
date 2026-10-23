using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the number of participants in the public event in objectId with value.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// Inferred from the name; an inactive event has none.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PublicEventParticipantCount)]
    public class PrerequisiteCheckPublicEventParticipantCount : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPublicEventParticipantCount> log;

        public PrerequisiteCheckPublicEventParticipantCount(
            ILogger<PrerequisiteCheckPublicEventParticipantCount> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            uint count = (uint)(subject.Map?.PublicEventManager.GetActiveEvent(objectId)?.GetTeams().Sum(t => t.GetMembers().Count()) ?? 0);

            if (!PrerequisiteComparer.TryCompare(comparison, count, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PublicEventParticipantCount}!");

            return result;
        }
    }
}
