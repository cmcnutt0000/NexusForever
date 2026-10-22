using System.Numerics;
using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the squared distance between the unit and the target with value squared.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 95) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Distance)]
    public class PrerequisiteCheckDistance : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckDistance> log;

        public PrerequisiteCheckDistance(
            ILogger<PrerequisiteCheckDistance> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (parameters.Target == null)
                return comparison == PrerequisiteComparison.NotEqual;

            float distanceSquared = Vector3.DistanceSquared(subject.Position, parameters.Target.Position);

            if (!PrerequisiteComparer.TryCompare(comparison, (ulong)distanceSquared, (ulong)value * value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Distance}!");

            return result;
        }
    }
}
