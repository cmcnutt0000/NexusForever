using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is in the world in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 109) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.WorldRequirement)]
    public class PrerequisiteCheckWorldRequirement : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckWorldRequirement> log;

        public PrerequisiteCheckWorldRequirement(
            ILogger<PrerequisiteCheckWorldRequirement> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool inWorld = subject.Map?.Entry?.Id == objectId;

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return inWorld;
                case PrerequisiteComparison.NotEqual:
                    return !inWorld;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.WorldRequirement}!");
                    return false;
            }
        }
    }
}
