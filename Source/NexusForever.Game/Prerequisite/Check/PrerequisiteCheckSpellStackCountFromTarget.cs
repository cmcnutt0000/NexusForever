using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the stack count of spells with the Spell4 id in objectId, cast by the target, affecting the unit with value.
    /// </summary>
    /// <remarks>
    /// See 0x14046A110 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.SpellStackCountFromTarget)]
    public class PrerequisiteCheckSpellStackCountFromTarget : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckSpellStackCountFromTarget> log;

        public PrerequisiteCheckSpellStackCountFromTarget(
            ILogger<PrerequisiteCheckSpellStackCountFromTarget> log)
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

            uint count = subject.GetAffectingSpellStackCount(objectId, target);

            if (!PrerequisiteComparer.TryCompare(comparison, count, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.SpellStackCountFromTarget}!");

            return result;
        }
    }
}
