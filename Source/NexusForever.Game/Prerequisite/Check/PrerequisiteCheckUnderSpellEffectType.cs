using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a spell effect of the <see cref="SpellEffectType"/> in objectId, from any caster, is active on the unit.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 105) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.UnderSpellEffectType)]
    public class PrerequisiteCheckUnderSpellEffectType : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnderSpellEffectType> log;

        public PrerequisiteCheckUnderSpellEffectType(
            ILogger<PrerequisiteCheckUnderSpellEffectType> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isAffected = subject.IsAffectedBySpellEffect(e => (uint)e.EffectType == objectId);

            if (!PrerequisiteComparer.TryEvaluate(comparison, isAffected, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.UnderSpellEffectType}!");

            return result;
        }
    }
}
