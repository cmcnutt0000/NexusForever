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
    /// See 0x1404A4EC0 in the client, which searches the auras on the unit rather than the spells it is casting.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.UnderSpell)]
    public class PrerequisiteCheckSpell : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckSpell> log;

        public PrerequisiteCheckSpell(
            ILogger<PrerequisiteCheckSpell> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isAffected = subject.IsAffectedBySpell(value);

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isAffected;
                case PrerequisiteComparison.NotEqual:
                    return !isAffected;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.UnderSpell}!");
                    return false;
            }
        }
    }
}
