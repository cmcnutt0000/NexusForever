using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a spell with the Spell4 id in value, cast by the target, is affecting the unit.
    /// </summary>
    /// <remarks>
    /// See 0x1404699F0 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.UnderSpellFromTarget)]
    public class PrerequisiteCheckUnderSpellFromTarget : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnderSpellFromTarget> log;

        public PrerequisiteCheckUnderSpellFromTarget(
            ILogger<PrerequisiteCheckUnderSpellFromTarget> log)
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

            bool isAffected = subject.IsAffectedBySpell(value, target);

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isAffected;
                case PrerequisiteComparison.NotEqual:
                    return !isAffected;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.UnderSpellFromTarget}!");
                    return false;
            }
        }
    }
}
