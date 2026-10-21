using System.Numerics;
using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Account;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;
using NexusForever.Network.World.Message.Static;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// The client compares the player's Healthy Gaming state (the play time limiter state sent to the UI with HealthyGamingUpdate) with objectId, see 0x14049CB30 in the client.
    /// </summary>
    /// <remarks>
    /// The only prerequisite using this type is 37806 (state not equal to 2), the cast and persistence prerequisite of Recall, Teleport to House, the capital teleports and Rapid Transport.
    /// Rapid Transport casts carry a taxi node, so the cost check for Rapid Transport also happens here.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HealthyGamingState)]
    public class PrerequisiteCheckHealthyGamingState : IPrerequisiteCheck
    {
        /// <summary>
        /// Healthy Gaming isn't implemented, players are always in the unrestricted state.
        /// </summary>
        private const uint HealthyGamingState = 0u;

        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHealthyGamingState> log;
        private readonly IGameTableManager gameTableManager;
        private readonly IRapidTransportCostCalculator rapidTransportCostCalculator;

        public PrerequisiteCheckHealthyGamingState(
            ILogger<PrerequisiteCheckHealthyGamingState> log,
            IGameTableManager gameTableManager,
            IRapidTransportCostCalculator rapidTransportCostCalculator)
        {
            this.log                          = log;
            this.gameTableManager             = gameTableManager;
            this.rapidTransportCostCalculator = rapidTransportCostCalculator;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            if (parameters.TaxiNode != null && !CanAffordRapidTransport(player, parameters))
            {
                parameters.CastResult ??= CastResult.RapidTransportInvalid;
                return false;
            }

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return HealthyGamingState == objectId;
                case PrerequisiteComparison.NotEqual:
                    return HealthyGamingState != objectId;
                case PrerequisiteComparison.GreaterThan:
                    return HealthyGamingState > objectId;
                case PrerequisiteComparison.GreaterThanOrEqual:
                    return HealthyGamingState >= objectId;
                case PrerequisiteComparison.LessThan:
                    return HealthyGamingState < objectId;
                case PrerequisiteComparison.LessThanOrEqual:
                    return HealthyGamingState <= objectId;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.HealthyGamingState}!");
                    return false;
            }
        }

        private bool CanAffordRapidTransport(IPlayer player, IPrerequisiteParameters parameters)
        {
            if (parameters.TaxiNode == null)
                return false;

            TaxiNodeEntry taxiNodeEntry = gameTableManager.TaxiNode.GetEntry(parameters.TaxiNode.Value);
            if (taxiNodeEntry == null)
                return false;

            if (player.Level < taxiNodeEntry.AutoUnlockLevel)
                return false;

            WorldLocation2Entry worldLocation = gameTableManager.WorldLocation2.GetEntry(taxiNodeEntry.WorldLocation2Id);
            if (worldLocation == null)
                return false;

            var destinationPosition = new Vector3(worldLocation.Position0, worldLocation.Position1, worldLocation.Position2);
            if (Vector3.Distance(player.Position, destinationPosition) < 200f)
                return false;

            GameFormulaEntry spellEntry = gameTableManager.GameFormula.GetEntry(1307);
            if (spellEntry == null)
                return false;

            if (player.SpellManager.GetSpellCooldown(spellEntry.Dataint0) > 0d)
            {
                ulong? serviceTokenPrice = rapidTransportCostCalculator.CalculateServiceTokenCost(parameters.TaxiNode.Value);
                if (serviceTokenPrice == null)
                    return false;

                if (!player.Account.CurrencyManager.CanAfford(AccountCurrencyType.ServiceToken, serviceTokenPrice.Value))
                {
                    parameters.CastResult = CastResult.ServiceTokensInsufficentFunds;
                    return false;
                }
            }
            else
            {
                ulong? creditPrice = rapidTransportCostCalculator.CalculateCreditCost(parameters.TaxiNode.Value, player.Map.Entry.Id, player.Position);
                if (creditPrice == null)
                    return false;

                if (!player.CurrencyManager.CanAfford(CurrencyType.Credits, creditPrice.Value))
                {
                    parameters.CastResult = CastResult.CasterVitalCostMoney;
                    return false;
                }
            }

            return true;
        }
    }
}
