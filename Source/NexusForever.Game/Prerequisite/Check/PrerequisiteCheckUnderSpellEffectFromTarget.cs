using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the spell effect with the Spell4Effects id in objectId, cast by the target, is active on the unit.
    /// </summary>
    /// <remarks>
    /// See 0x14046A190 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.UnderSpellEffectFromTarget)]
    public class PrerequisiteCheckUnderSpellEffectFromTarget : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnderSpellEffectFromTarget> log;

        public PrerequisiteCheckUnderSpellEffectFromTarget(
            ILogger<PrerequisiteCheckUnderSpellEffectFromTarget> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // the client passes NotEqual and fails everything else when there is no target
            IUnitEntity target = parameters?.Target;
            if (target == null)
                return comparison == PrerequisiteComparison.NotEqual;

            bool isAffected = subject.IsAffectedBySpellEffect(e => e.Id == objectId, target);

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isAffected;
                case PrerequisiteComparison.NotEqual:
                    return !isAffected;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.UnderSpellEffectFromTarget}!");
                    return false;
            }
        }
    }
}
