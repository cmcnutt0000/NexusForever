using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Reputation;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the faction of the unit's summoner, or the unit itself without one, against value.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// Inferred from the data: only used by the Engineer's Repairbot and Artillerybot next to a <see cref="PrerequisiteType.BaseFaction"/> check.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.SummonerFaction)]
    public class PrerequisiteCheckSummonerFaction : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckSummonerFaction> log;

        public PrerequisiteCheckSummonerFaction(
            ILogger<PrerequisiteCheckSummonerFaction> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IWorldEntity summoner = subject.SummonerGuid is uint summonerGuid ? subject.GetVisible<IWorldEntity>(summonerGuid) : null;
            Faction faction = (summoner ?? subject).Faction1;

            if (!PrerequisiteComparer.TryEvaluate(comparison, faction == (Faction)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.SummonerFaction}!");

            return result;
        }
    }
}
