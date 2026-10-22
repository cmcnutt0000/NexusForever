using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Entity;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is a player.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 39) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.IsPlayer)]
    public class PrerequisiteCheckIsPlayer : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckIsPlayer> log;

        public PrerequisiteCheckIsPlayer(
            ILogger<PrerequisiteCheckIsPlayer> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isPlayer = subject.Type == EntityType.Player;

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isPlayer;
                case PrerequisiteComparison.NotEqual:
                    return !isPlayer;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.IsPlayer}!");
                    return false;
            }
        }
    }
}
