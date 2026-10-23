using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a spell with the Spell4 id in value, from any caster, is affecting the unit.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// Inferred from the data: value is a Spell4 id in 1,554 of 1,788 conditions and spell targeting, persistence and gossip use it as
    /// "under aura" (for example AoE targets under Block are skipped). objectId is set on 138 conditions and isn't identified.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.UnderSpellServer)]
    public class PrerequisiteCheckUnderSpellServer : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnderSpellServer> log;

        public PrerequisiteCheckUnderSpellServer(
            ILogger<PrerequisiteCheckUnderSpellServer> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, subject.IsAffectedBySpell(value), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.UnderSpellServer}!");

            return result;
        }
    }
}
