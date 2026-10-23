using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the player's personal battleground rating with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049DD10 in the client.
    /// TODO: NexusForever doesn't track PvP ratings, they are 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Battlegrounds254)]
    public class PrerequisiteCheckBattlegrounds254 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckBattlegrounds254> log;

        public PrerequisiteCheckBattlegrounds254(
            ILogger<PrerequisiteCheckBattlegrounds254> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Battlegrounds254}!");

            return result;
        }
    }
}
