using System.Numerics;
using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a creature with the id in objectId is within value of the unit.
    /// </summary>
    /// <remarks>
    /// See 0x14049ED50 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.CreatureWithinDistance)]
    public class PrerequisiteCheckCreatureWithinDistance : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckCreatureWithinDistance> log;

        public PrerequisiteCheckCreatureWithinDistance(
            ILogger<PrerequisiteCheckCreatureWithinDistance> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isNearby = subject.GetVisibleCreature<IWorldEntity>(objectId)
                .Any(e => Vector3.DistanceSquared(subject.Position, e.Position) <= (float)value * value);

            if (!PrerequisiteComparer.TryEvaluate(comparison, isNearby, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.CreatureWithinDistance}!");

            return result;
        }
    }
}
