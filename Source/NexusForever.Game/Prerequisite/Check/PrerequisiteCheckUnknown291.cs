using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares a per-world counter with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A1890 in the client (capped by World row fields +0x48 to +0x50), only the player passes.
    /// TODO: NexusForever doesn't track this counter, it is 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Unknown291)]
    public class PrerequisiteCheckUnknown291 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnknown291> log;

        public PrerequisiteCheckUnknown291(
            ILogger<PrerequisiteCheckUnknown291> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Unknown291}!");

            return result;
        }
    }
}
