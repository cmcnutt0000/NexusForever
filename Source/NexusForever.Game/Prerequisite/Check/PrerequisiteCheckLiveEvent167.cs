using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the live event in objectId is active.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// TODO: NexusForever doesn't track live events. It fails as the client does.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.LiveEvent167)]
    public class PrerequisiteCheckLiveEvent167 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckLiveEvent167> log;

        public PrerequisiteCheckLiveEvent167(
            ILogger<PrerequisiteCheckLiveEvent167> log)
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
