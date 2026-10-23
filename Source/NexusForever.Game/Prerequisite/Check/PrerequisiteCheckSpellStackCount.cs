using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the stack count of spells with the Spell4 id in objectId affecting the unit with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A4F60 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.SpellStackCount)]
    public class PrerequisiteCheckSpellStackCount : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckSpellStackCount> log;

        public PrerequisiteCheckSpellStackCount(
            ILogger<PrerequisiteCheckSpellStackCount> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            uint count = subject.GetAffectingSpellStackCount(objectId);

            if (!PrerequisiteComparer.TryCompare(comparison, count, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.SpellStackCount}!");

            return result;
        }
    }
}
