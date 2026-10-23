using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks a field of the item being evaluated (+0x148) against objectId.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0D30 in the client, used by achievement objectives.
    /// TODO: NexusForever doesn't pass the item being evaluated, the client fails every comparison without one.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Item134)]
    public class PrerequisiteCheckItem134 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItem134> log;

        public PrerequisiteCheckItem134(
            ILogger<PrerequisiteCheckItem134> log)
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
