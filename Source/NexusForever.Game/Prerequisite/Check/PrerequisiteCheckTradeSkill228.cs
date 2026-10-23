using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player has learned the tradeskill in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x14049F310 in the client.
    /// TODO: NexusForever doesn't track tradeskills, nothing is learned.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.TradeSkill228)]
    public class PrerequisiteCheckTradeSkill228 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckTradeSkill228> log;

        public PrerequisiteCheckTradeSkill228(
            ILogger<PrerequisiteCheckTradeSkill228> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.TradeSkill228}!");

            return result;
        }
    }
}
