using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is a player.
    /// </summary>
    /// <remarks>
    /// See 0x14049C7B0 in the client, which compares with the local player. The server has no local player, so any player counts; only VisualEffect uses this.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.IsLocalPlayer)]
    public class PrerequisiteCheckIsLocalPlayer : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckIsLocalPlayer> log;

        public PrerequisiteCheckIsLocalPlayer(
            ILogger<PrerequisiteCheckIsLocalPlayer> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, subject is IPlayer, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.IsLocalPlayer}!");

            return result;
        }
    }
}
