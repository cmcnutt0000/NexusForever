using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the spell effect with the Spell4Effects id in objectId, from any caster, is active on the unit.
    /// </summary>
    /// <remarks>
    /// See 0x1404A4FE0 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.UnderSpellEffect)]
    public class PrerequisiteCheckUnderSpellEffect : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnderSpellEffect> log;

        public PrerequisiteCheckUnderSpellEffect(
            ILogger<PrerequisiteCheckUnderSpellEffect> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isAffected = subject.IsAffectedBySpellEffect(e => e.Id == objectId);

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isAffected;
                case PrerequisiteComparison.NotEqual:
                    return !isAffected;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.UnderSpellEffect}!");
                    return false;
            }
        }
    }
}
