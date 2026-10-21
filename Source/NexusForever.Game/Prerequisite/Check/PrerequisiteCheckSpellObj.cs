using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
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
            // objectId is a spell group id (Spell4GroupList), the client checks for an aura on the unit from a spell in that group, see 0x1404A2B5A in the client
            // TODO: the client also counts auras applied by other units, spells are currently only tracked on the caster
            bool hasSpell = subject.GetSpellsByGroupId(objectId).Any(s => !s.IsCasting && !s.IsFinished);

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return hasSpell;
                case PrerequisiteComparison.NotEqual:
                    return !hasSpell;
                default:
                    log.LogWarning($"Unhandled {comparison} for {PrerequisiteType.SpellObj}!");
                    return false;
            }
        }
    }
}
