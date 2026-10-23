using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks a field of the item being evaluated (+0x140) against objectId.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0D00 in the client, used by achievement objectives.
    /// TODO: NexusForever doesn't pass the item being evaluated, the client fails every comparison without one.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Item133)]
    public class PrerequisiteCheckItem133 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItem133> log;

        public PrerequisiteCheckItem133(
            ILogger<PrerequisiteCheckItem133> log)
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
