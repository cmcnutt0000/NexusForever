using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the state of the settler infrastructure in objectId against value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0150 in the client, unknown infrastructure only passes <see cref="PrerequisiteComparison.NotEqual"/>.
    /// TODO: NexusForever doesn't track settler infrastructure.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.InfrastructureState)]
    public class PrerequisiteCheckInfrastructureState : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckInfrastructureState> log;

        public PrerequisiteCheckInfrastructureState(
            ILogger<PrerequisiteCheckInfrastructureState> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            return comparison == PrerequisiteComparison.NotEqual;
        }
    }
}
