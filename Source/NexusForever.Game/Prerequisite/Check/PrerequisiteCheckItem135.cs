using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks a field of the item being evaluated (+0x144) against objectId.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0D60 in the client, used by achievement objectives.
    /// TODO: NexusForever doesn't pass the item being evaluated, the client fails every comparison without one.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Item135)]
    public class PrerequisiteCheckItem135 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItem135> log;

        public PrerequisiteCheckItem135(
            ILogger<PrerequisiteCheckItem135> log)
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
