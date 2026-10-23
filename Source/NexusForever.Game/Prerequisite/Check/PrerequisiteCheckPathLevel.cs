using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the level of the player's current path with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049FFB0 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PathLevel)]
    public class PrerequisiteCheckPathLevel : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPathLevel> log;

        public PrerequisiteCheckPathLevel(
            ILogger<PrerequisiteCheckPathLevel> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            uint level = subject is IPlayer player ? player.PathManager.GetLevel(player.Path) : 0u;

            if (!PrerequisiteComparer.TryCompare(comparison, level, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PathLevel}!");

            return result;
        }
    }
}
