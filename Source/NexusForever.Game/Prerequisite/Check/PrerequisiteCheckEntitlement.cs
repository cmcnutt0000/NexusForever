using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player has the entitlement in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x1404A1620 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Entitlement)]
    public class PrerequisiteCheckEntitlement : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckEntitlement> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckEntitlement(
            ILogger<PrerequisiteCheckEntitlement> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // only players and ghosts pass
            if (subject is not IPlayer player || subject.Type is not EntityType.Player and not EntityType.Ghost)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, GetCount(player, objectId) != 0u, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Entitlement}!");

            return result;
        }

        // character and account entitlements add up, capped at the entitlement's maximum, see 0x1403D2140 in the client
        private uint GetCount(IPlayer player, uint entitlementId)
        {
            uint count = (player.EntitlementManager.GetEntitlement((EntitlementType)entitlementId)?.Amount ?? 0u)
                + (player.Account.EntitlementManager.GetEntitlement((EntitlementType)entitlementId)?.Amount ?? 0u);

            uint maximum = gameTableManager.Entitlement.GetEntry(entitlementId)?.MaxCount ?? 0u;
            return maximum != 0u ? Math.Min(count, maximum) : count;
        }
    }
}
