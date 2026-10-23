using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks both the unit and the target as <see cref="PrerequisiteCheckSpell"/>.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 279) in the client, which fails when there is no target.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.UnderSpellAndTarget)]
    public class PrerequisiteCheckUnderSpellAndTarget : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnderSpellAndTarget> log;

        public PrerequisiteCheckUnderSpellAndTarget(
            ILogger<PrerequisiteCheckUnderSpellAndTarget> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IUnitEntity target = parameters?.Target;
            if (target == null)
                return false;

            return Meets(subject, comparison, value) && Meets(target, comparison, value);
        }

        private bool Meets(IUnitEntity unit, PrerequisiteComparison comparison, uint value)
        {
            bool isAffected = unit.IsAffectedBySpell(value);

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isAffected;
                case PrerequisiteComparison.NotEqual:
                    return !isAffected;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.UnderSpellAndTarget}!");
                    return false;
            }
        }
    }
}
