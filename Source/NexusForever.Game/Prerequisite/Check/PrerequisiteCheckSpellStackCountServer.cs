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
    /// Server-only, the client has no implementation and always fails it.
    /// Inferred from the data: objectId is a Spell4 id in 260 of 281 conditions, paired with <see cref="PrerequisiteType.UnderSpellServer"/> and small counts.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.SpellStackCountServer)]
    public class PrerequisiteCheckSpellStackCountServer : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckSpellStackCountServer> log;

        public PrerequisiteCheckSpellStackCountServer(
            ILogger<PrerequisiteCheckSpellStackCountServer> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, subject.GetAffectingSpellStackCount(objectId), value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.SpellStackCountServer}!");

            return result;
        }
    }
}
