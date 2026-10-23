using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the upgrade tier of the structured plug with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A1340 in the client (unit+0x15AC).
    /// TODO: NexusForever doesn't track plug upgrades, the tier is 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.WarplotPlugUpgrade)]
    public class PrerequisiteCheckWarplotPlugUpgrade : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckWarplotPlugUpgrade> log;

        public PrerequisiteCheckWarplotPlugUpgrade(
            ILogger<PrerequisiteCheckWarplotPlugUpgrade> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject.Type != EntityType.StructuredPlug)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.WarplotPlugUpgrade}!");

            return result;
        }
    }
}
