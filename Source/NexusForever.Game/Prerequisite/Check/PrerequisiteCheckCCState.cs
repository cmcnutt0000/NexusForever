using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Combat.CrowdControl;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit has the <see cref="CCState"/> in value, see 0x1404A2100 (case 56) in the client.
    /// </summary>
    /// <remarks>
    /// Used by the cast prerequisite of Urgent Withdrawal (not rooted) and by many persistence prerequisites.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.CCState)]
    public class PrerequisiteCheckCCState : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckCCState> log;

        public PrerequisiteCheckCCState(
            ILogger<PrerequisiteCheckCCState> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // the client has 28 CC state slots, other values are never set
            bool hasState = value < 28u && subject.CrowdControlManager.GetCCEffect((CCState)value) != null;

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return hasState;
                case PrerequisiteComparison.NotEqual:
                    return !hasState;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.CCState}!");
                    return false;
            }
        }
    }
}
