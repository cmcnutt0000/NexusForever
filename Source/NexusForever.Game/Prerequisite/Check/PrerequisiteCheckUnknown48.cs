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
    /// Used by Berserk persistence (1 condition used); the data doesn't show what it checks, so it fails as the client does.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Unknown48)]
    public class PrerequisiteCheckUnknown48 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnknown48> log;

        public PrerequisiteCheckUnknown48(
            ILogger<PrerequisiteCheckUnknown48> log)
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
