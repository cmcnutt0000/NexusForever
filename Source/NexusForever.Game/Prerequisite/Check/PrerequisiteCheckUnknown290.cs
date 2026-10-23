using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the world field next to the difficulty with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049EFA0 in the client (world+0x6428), which isn't identified. NexusForever has no equivalent, so it is 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Unknown290)]
    public class PrerequisiteCheckUnknown290 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnknown290> log;

        public PrerequisiteCheckUnknown290(
            ILogger<PrerequisiteCheckUnknown290> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Unknown290}!");

            return result;
        }
    }
}
