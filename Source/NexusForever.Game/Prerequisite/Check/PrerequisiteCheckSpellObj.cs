using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a spell in the spell group in objectId, from any caster, is affecting the unit.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 129) in the client, objectId is a Spell4GroupList spell group id.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.SpellObj)]
    internal class PrerequisiteCheckSpellObj : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckSpellObj> log;

        public PrerequisiteCheckSpellObj(
            ILogger<PrerequisiteCheckSpellObj> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isAffected = subject.IsAffectedBySpellGroup(objectId);

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isAffected;
                case PrerequisiteComparison.NotEqual:
                    return !isAffected;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.SpellObj}!");
                    return false;
            }
        }
    }
}
