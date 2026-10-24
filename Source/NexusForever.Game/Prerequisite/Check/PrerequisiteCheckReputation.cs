using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.Reputation;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Reputation;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's reputation with the faction in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 5) and 0x140787690 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Reputation)]
    public class PrerequisiteCheckReputation : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckReputation> log;
        private readonly IFactionManager factionManager;

        public PrerequisiteCheckReputation(
            ILogger<PrerequisiteCheckReputation> log,
            IFactionManager factionManager)
        {
            this.log            = log;
            this.factionManager = factionManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            float reputation = GetReputation(subject, (Faction)objectId);

            if (!PrerequisiteComparer.TryCompare(comparison, reputation, (float)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Reputation}!");

            return result;
        }

        private float GetReputation(IUnitEntity unit, Faction factionId)
        {
            IFactionNode targetNode = factionManager.GetFaction(factionId);
            if (targetNode == null)
                return 0f;

            // only the player's base faction reads the reputation sent by the server (0x1403B5AD0), a temporary faction doesn't
            if (unit is IPlayer player && unit.Faction1 == unit.Faction2)
            {
                // a temporary override is read before the reputation (0x1403CAC00)
                float? reputationOverride = player.ReputationManager.GetReputationOverride(factionId);
                if (reputationOverride != null)
                    return reputationOverride.Value;

                IReputation reputation = player.ReputationManager.GetReputation(factionId);
                if (reputation != null)
                    return reputation.Amount;
            }

            // otherwise the default of the unit's current faction
            return factionManager.GetFaction(unit.Faction1)?.GetDefaultReputation(targetNode) ?? 0f;
        }
    }
}
