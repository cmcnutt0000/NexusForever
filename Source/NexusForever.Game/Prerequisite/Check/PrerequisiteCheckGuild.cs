using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Guild;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit belongs to a guild or circle.
    /// </summary>
    /// <remarks>
    /// See 0x14049E9E0 in the client (unit+0xA8).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Guild)]
    public class PrerequisiteCheckGuild : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckGuild> log;

        public PrerequisiteCheckGuild(
            ILogger<PrerequisiteCheckGuild> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, subject is IPlayer player && player.GuildManager.Any(g => g.Type is GuildType.Guild or GuildType.Circle), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Guild}!");

            return result;
        }
    }
}
