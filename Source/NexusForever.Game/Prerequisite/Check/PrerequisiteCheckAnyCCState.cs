using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit has any CC state, see 0x1404A2100 (case 77) in the client.
    /// </summary>
    [PrerequisiteCheck(PrerequisiteType.AnyCCState)]
    public class PrerequisiteCheckAnyCCState : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckAnyCCState> log;

        public PrerequisiteCheckAnyCCState(
            ILogger<PrerequisiteCheckAnyCCState> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool hasState = subject.CrowdControlManager.GetCCStates().Any();

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return hasState;
                case PrerequisiteComparison.NotEqual:
                    return !hasState;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.AnyCCState}!");
                    return false;
            }
        }
    }
}
