using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the security progress percentage of the settler hub in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A02E0 in the client, an unknown hub fails every comparison.
    /// TODO: NexusForever doesn't track settler hubs.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HubSecurity)]
    public class PrerequisiteCheckHubSecurity : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHubSecurity> log;

        public PrerequisiteCheckHubSecurity(
            ILogger<PrerequisiteCheckHubSecurity> log)
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
