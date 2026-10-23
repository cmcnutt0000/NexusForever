using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is stealthed.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 116) in the client, which tests a stealth level above 1 (unit+0x54).
    /// NexusForever has no stealth level, a <see cref="SpellEffectType.Stealth"/> effect counts instead.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Stealth)]
    public class PrerequisiteCheckStealth : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckStealth> log;

        public PrerequisiteCheckStealth(
            ILogger<PrerequisiteCheckStealth> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isStealthed = subject.IsAffectedBySpellEffect(e => e.EffectType == SpellEffectType.Stealth);

            if (!PrerequisiteComparer.TryEvaluate(comparison, isStealthed, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Stealth}!");

            return result;
        }
    }
}
