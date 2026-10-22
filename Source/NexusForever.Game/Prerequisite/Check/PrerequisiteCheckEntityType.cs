using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is the <see cref="Static.Entity.EntityType"/> in value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 82) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.EntityType)]
    public class PrerequisiteCheckEntityType : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckEntityType> log;

        public PrerequisiteCheckEntityType(
            ILogger<PrerequisiteCheckEntityType> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool matches = (uint)subject.Type == value;

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return matches;
                case PrerequisiteComparison.NotEqual:
                    return !matches;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.EntityType}!");
                    return false;
            }
        }
    }
}
