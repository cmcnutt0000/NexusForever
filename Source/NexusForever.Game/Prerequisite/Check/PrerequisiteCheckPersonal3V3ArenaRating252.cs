using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the player's personal 3v3 arena rating with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049DC50 in the client.
    /// TODO: NexusForever doesn't track PvP ratings, they are 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Personal3V3ArenaRating252)]
    public class PrerequisiteCheckPersonal3V3ArenaRating252 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPersonal3V3ArenaRating252> log;

        public PrerequisiteCheckPersonal3V3ArenaRating252(
            ILogger<PrerequisiteCheckPersonal3V3ArenaRating252> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Personal3V3ArenaRating252}!");

            return result;
        }
    }
}
