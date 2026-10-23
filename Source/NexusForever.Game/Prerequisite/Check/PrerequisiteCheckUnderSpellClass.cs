using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a spell of the <see cref="SpellClass"/> in objectId, from any caster, is affecting the unit.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 93) in the client, used by cleanses to prefer targets with a dispellable buff or debuff.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.UnderSpellClass)]
    public class PrerequisiteCheckUnderSpellClass : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnderSpellClass> log;

        public PrerequisiteCheckUnderSpellClass(
            ILogger<PrerequisiteCheckUnderSpellClass> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isAffected = subject.IsAffectedBySpell(s => (uint)s.Parameters.SpellInfo.BaseInfo.Entry.SpellClass == objectId);

            if (!PrerequisiteComparer.TryEvaluate(comparison, isAffected, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.UnderSpellClass}!");

            return result;
        }
    }
}
