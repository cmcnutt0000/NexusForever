using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player is in a duel.
    /// </summary>
    /// <remarks>
    /// See 0x14049DB50 in the client (duel state 3).
    /// TODO: NexusForever has no duels.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Dueling)]
    public class PrerequisiteCheckDueling : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckDueling> log;

        public PrerequisiteCheckDueling(
            ILogger<PrerequisiteCheckDueling> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Dueling}!");

            return result;
        }
    }
}
