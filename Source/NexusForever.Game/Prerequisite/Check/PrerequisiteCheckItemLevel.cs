using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the level of the item being evaluated with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0DB0 in the client, used by achievement objectives.
    /// TODO: NexusForever doesn't pass the item being evaluated, the client fails every comparison without one.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ItemLevel)]
    public class PrerequisiteCheckItemLevel : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItemLevel> log;

        public PrerequisiteCheckItemLevel(
            ILogger<PrerequisiteCheckItemLevel> log)
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
