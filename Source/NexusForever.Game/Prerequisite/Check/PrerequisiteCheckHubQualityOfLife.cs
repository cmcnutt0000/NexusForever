using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the quality of life progress percentage of the settler hub in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0260 in the client, an unknown hub fails every comparison.
    /// TODO: NexusForever doesn't track settler hubs.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HubQualityOfLife)]
    public class PrerequisiteCheckHubQualityOfLife : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHubQualityOfLife> log;

        public PrerequisiteCheckHubQualityOfLife(
            ILogger<PrerequisiteCheckHubQualityOfLife> log)
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
