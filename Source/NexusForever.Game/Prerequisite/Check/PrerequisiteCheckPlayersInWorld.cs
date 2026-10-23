using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Map.Search;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a player on the unit's map meets the prerequisite in objectId.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it. ("There are no players in the world who meet the requirement").
    /// Inferred from the data: every objectId is a prerequisite id.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PlayersInWorld)]
    public class PrerequisiteCheckPlayersInWorld : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPlayersInWorld> log;
        private readonly IPrerequisiteManager prerequisiteManager;

        public PrerequisiteCheckPlayersInWorld(
            ILogger<PrerequisiteCheckPlayersInWorld> log,
            IPrerequisiteManager prerequisiteManager)
        {
            this.log                 = log;
            this.prerequisiteManager = prerequisiteManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject.Map == null || objectId == 0u)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, subject.Map.Search(subject.Position, null, new AnyPlayer()).Any(p => prerequisiteManager.Meets(p, objectId)), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PlayersInWorld}!");

            return result;
        }

        private sealed class AnyPlayer : ISearchCheck<IPlayer>
        {
            public bool CheckEntity(IPlayer entity) => true;
        }
    }
}
