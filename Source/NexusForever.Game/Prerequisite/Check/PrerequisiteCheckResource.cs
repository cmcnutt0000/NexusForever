using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's class resource with the index in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 64) in the client, the 11 resources at unit+0x21C (same as Lua Unit:GetResource).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Resource)]
    public class PrerequisiteCheckResource : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckResource> log;

        public PrerequisiteCheckResource(
            ILogger<PrerequisiteCheckResource> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, subject.GetResource(objectId), (float)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Resource}!");

            return result;
        }
    }
}
