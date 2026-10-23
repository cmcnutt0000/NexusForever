using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares an account field with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A1710 in the client (account+0x180), which isn't identified. NexusForever has no equivalent, so it is 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Unknown286)]
    public class PrerequisiteCheckUnknown286 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnknown286> log;

        public PrerequisiteCheckUnknown286(
            ILogger<PrerequisiteCheckUnknown286> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // only players and ghosts pass
            if (subject.Type is not EntityType.Player and not EntityType.Ghost)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Unknown286}!");

            return result;
        }
    }
}
