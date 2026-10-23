using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a combat event allowed by the Spell4Prerequisites in objectId happened within value milliseconds.
    /// </summary>
    /// <remarks>
    /// See 0x14049D5B0 in the client, 0 milliseconds uses GameFormula 100.
    /// TODO: NexusForever doesn't keep recent combat events.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.SpellCondition92)]
    public class PrerequisiteCheckSpellCondition92 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckSpellCondition92> log;

        public PrerequisiteCheckSpellCondition92(
            ILogger<PrerequisiteCheckSpellCondition92> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.SpellCondition92}!");

            return result;
        }
    }
}
