using System.Numerics;
using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's distance to the world location in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 16) in the client.
    /// Every comparison fails when the world location is in another world.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.DistanceToWorldLocation)]
    public class PrerequisiteCheckDistanceToWorldLocation : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckDistanceToWorldLocation> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckDistanceToWorldLocation(
            ILogger<PrerequisiteCheckDistanceToWorldLocation> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            WorldLocation2Entry entry = gameTableManager.WorldLocation2.GetEntry(objectId);
            if (entry == null || subject.Map?.Entry?.Id != entry.WorldId)
                return false;

            float distance = Vector3.Distance(subject.Position, new Vector3(entry.Position0, entry.Position1, entry.Position2));

            if (!PrerequisiteComparer.TryCompare(comparison, distance, (float)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.DistanceToWorldLocation}!");

            return result;
        }
    }
}
