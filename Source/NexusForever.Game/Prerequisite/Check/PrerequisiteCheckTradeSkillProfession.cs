using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the player's tier in the tradeskill of the TradeskillTier in objectId with that row's tier.
    /// </summary>
    /// <remarks>
    /// See 0x14049F0F0 in the client, which fails when the tradeskill isn't learned.
    /// TODO: NexusForever doesn't track tradeskills, nothing is learned.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.TradeSkillProfession)]
    public class PrerequisiteCheckTradeSkillProfession : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckTradeSkillProfession> log;

        public PrerequisiteCheckTradeSkillProfession(
            ILogger<PrerequisiteCheckTradeSkillProfession> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            return false;
        }
    }
}
