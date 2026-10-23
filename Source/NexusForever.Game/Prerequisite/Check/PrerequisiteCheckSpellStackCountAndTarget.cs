using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks both the unit and the target as <see cref="PrerequisiteCheckSpellStackCount"/>.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 280) in the client, which fails when there is no target.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.SpellStackCountAndTarget)]
    public class PrerequisiteCheckSpellStackCountAndTarget : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckSpellStackCountAndTarget> log;

        public PrerequisiteCheckSpellStackCountAndTarget(
            ILogger<PrerequisiteCheckSpellStackCountAndTarget> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IUnitEntity target = parameters?.Target;
            if (target == null)
                return false;

            return Meets(subject, comparison, value, objectId) && Meets(target, comparison, value, objectId);
        }

        private bool Meets(IUnitEntity unit, PrerequisiteComparison comparison, uint value, uint objectId)
        {
            uint count = unit.GetAffectingSpellStackCount(objectId);

            if (!PrerequisiteComparer.TryCompare(comparison, count, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.SpellStackCountAndTarget}!");

            return result;
        }
    }
}
