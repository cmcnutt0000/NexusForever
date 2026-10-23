using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a spell in the spell group in objectId, cast by the target, is affecting the unit.
    /// </summary>
    /// <remarks>
    /// See 0x140469A70 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.SpellObjFromTarget)]
    public class PrerequisiteCheckSpellObjFromTarget : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckSpellObjFromTarget> log;

        public PrerequisiteCheckSpellObjFromTarget(
            ILogger<PrerequisiteCheckSpellObjFromTarget> log)
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

            bool isAffected = subject.IsAffectedBySpellGroup(objectId, target);

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isAffected;
                case PrerequisiteComparison.NotEqual:
                    return !isAffected;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.SpellObjFromTarget}!");
                    return false;
            }
        }
    }
}
