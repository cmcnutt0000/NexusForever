using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Unidentified server-only check.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// Used by Target Acquisition (1 condition used); the data doesn't show what it checks, so it fails as the client does.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Unknown271)]
    public class PrerequisiteCheckUnknown271 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnknown271> log;

        public PrerequisiteCheckUnknown271(
            ILogger<PrerequisiteCheckUnknown271> log)
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
