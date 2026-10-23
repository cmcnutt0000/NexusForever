using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the player's personal 5v5 arena rating with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049DCB0 in the client.
    /// TODO: NexusForever doesn't track PvP ratings, they are 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Personal5V5ArenaRating253)]
    public class PrerequisiteCheckPersonal5V5ArenaRating253 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPersonal5V5ArenaRating253> log;

        public PrerequisiteCheckPersonal5V5ArenaRating253(
            ILogger<PrerequisiteCheckPersonal5V5ArenaRating253> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Personal5V5ArenaRating253}!");

            return result;
        }
    }
}
