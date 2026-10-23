using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the tier of the item being evaluated against the TradeskillTier in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0F90 in the client, used by achievement objectives.
    /// TODO: NexusForever doesn't pass the item being evaluated, the client fails every comparison without one.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ItemTradeSkillLevel)]
    public class PrerequisiteCheckItemTradeSkillLevel : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItemTradeSkillLevel> log;

        public PrerequisiteCheckItemTradeSkillLevel(
            ILogger<PrerequisiteCheckItemTradeSkillLevel> log)
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
