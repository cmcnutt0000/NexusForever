using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if objectId is in the list of a learned tradeskill.
    /// </summary>
    /// <remarks>
    /// See 0x14049F190 in the client.
    /// TODO: NexusForever doesn't track tradeskills, nothing is learned.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.TradeSkill)]
    public class PrerequisiteCheckTradeSkill : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckTradeSkill> log;

        public PrerequisiteCheckTradeSkill(
            ILogger<PrerequisiteCheckTradeSkill> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.TradeSkill}!");

            return result;
        }
    }
}
