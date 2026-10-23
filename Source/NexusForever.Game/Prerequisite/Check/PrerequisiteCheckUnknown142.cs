using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks a state of the spell being cast against value.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// TODO: only used by Spellslinger spells (True Shot, Snake Eyes, Concussive Shot), probably Spell Surge, which NexusForever doesn't model. It fails as the client does.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Unknown142)]
    public class PrerequisiteCheckUnknown142 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnknown142> log;

        public PrerequisiteCheckUnknown142(
            ILogger<PrerequisiteCheckUnknown142> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            return false;
        }
    }
}
