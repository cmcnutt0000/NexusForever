using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Guild;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Guild;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks bit value of the player's warparty rank permissions.
    /// </summary>
    /// <remarks>
    /// See 0x14049EBF0 in the client, a player without a warparty fails every comparison.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.WarplotPermission)]
    public class PrerequisiteCheckWarplotPermission : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckWarplotPermission> log;

        public PrerequisiteCheckWarplotPermission(
            ILogger<PrerequisiteCheckWarplotPermission> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            IGuildRank rank = player.GuildManager.GetGuild<IWarParty>(GuildType.WarParty)?.GetMember(player.CharacterId)?.Rank;
            if (rank == null)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, value < 32u && ((uint)rank.Permissions & (1u << (int)value)) != 0u, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.WarplotPermission}!");

            return result;
        }
    }
}
