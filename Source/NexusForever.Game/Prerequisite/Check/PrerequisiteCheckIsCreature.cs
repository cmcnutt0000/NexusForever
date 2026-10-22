using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is the creature in value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 38) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.IsCreature)]
    public class PrerequisiteCheckIsCreature : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckIsCreature> log;

        public PrerequisiteCheckIsCreature(
            ILogger<PrerequisiteCheckIsCreature> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isCreature = subject.CreatureId == value;

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isCreature;
                case PrerequisiteComparison.NotEqual:
                    return !isCreature;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.IsCreature}!");
                    return false;
            }
        }
    }
}
