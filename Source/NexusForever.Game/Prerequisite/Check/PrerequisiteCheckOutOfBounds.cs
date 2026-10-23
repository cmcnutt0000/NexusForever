using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is out of bounds of value.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// TODO: the arguments aren't identified. It fails as the client does.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.OutOfBounds)]
    public class PrerequisiteCheckOutOfBounds : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckOutOfBounds> log;

        public PrerequisiteCheckOutOfBounds(
            ILogger<PrerequisiteCheckOutOfBounds> log)
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
