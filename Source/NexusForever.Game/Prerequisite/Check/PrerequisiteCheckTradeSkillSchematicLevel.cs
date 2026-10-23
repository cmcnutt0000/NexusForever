using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Always fails.
    /// </summary>
    /// <remarks>
    /// 0x1404A2100 has no case for this type, so the client fails it.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.TradeSkillSchematicLevel)]
    public class PrerequisiteCheckTradeSkillSchematicLevel : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckTradeSkillSchematicLevel> log;

        public PrerequisiteCheckTradeSkillSchematicLevel(
            ILogger<PrerequisiteCheckTradeSkillSchematicLevel> log)
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
