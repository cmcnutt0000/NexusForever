using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Guild;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Guild;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the rating of the player's warparty with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049DDD0 in the client, a player without a warparty fails every comparison.
    /// TODO: NexusForever doesn't track PvP ratings, they are 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.WarplotRating)]
    public class PrerequisiteCheckWarplotRating : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckWarplotRating> log;

        public PrerequisiteCheckWarplotRating(
            ILogger<PrerequisiteCheckWarplotRating> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player || player.GuildManager.GetGuild<IWarParty>(GuildType.WarParty) == null)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.WarplotRating}!");

            return result;
        }
    }
}
