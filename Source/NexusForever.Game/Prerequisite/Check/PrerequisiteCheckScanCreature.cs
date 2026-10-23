using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the target can be scanned for the scan bits in value.
    /// </summary>
    /// <remarks>
    /// See 0x14049FF30 in the client, with no target only <see cref="PrerequisiteComparison.NotEqual"/> passes.
    /// TODO: NexusForever doesn't send units as scannable, so the client never sees one.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ScanCreature)]
    public class PrerequisiteCheckScanCreature : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckScanCreature> log;

        public PrerequisiteCheckScanCreature(
            ILogger<PrerequisiteCheckScanCreature> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (parameters?.Target == null)
                return comparison == PrerequisiteComparison.NotEqual;

            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ScanCreature}!");

            return result;
        }
    }
}
