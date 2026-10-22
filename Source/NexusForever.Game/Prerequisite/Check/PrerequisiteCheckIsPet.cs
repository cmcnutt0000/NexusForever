using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Entity;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is a pet.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 266) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.IsPet)]
    public class PrerequisiteCheckIsPet : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckIsPet> log;

        public PrerequisiteCheckIsPet(
            ILogger<PrerequisiteCheckIsPet> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isPet = subject.Type is EntityType.Pet or EntityType.EsperPet;

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isPet;
                case PrerequisiteComparison.NotEqual:
                    return !isPet;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.IsPet}!");
                    return false;
            }
        }
    }
}
