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
    /// Checks if the unit is inside the radius of the world location in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 141) in the client.
    /// Equal passes inside the radius, both comparisons fail when the world location is in another world.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.IsOutOfBounds)]
    public class PrerequisiteCheckIsOutOfBounds : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckIsOutOfBounds> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckIsOutOfBounds(
            ILogger<PrerequisiteCheckIsOutOfBounds> log,
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
            bool isInside = distance < entry.Radius;

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isInside;
                case PrerequisiteComparison.NotEqual:
                    return !isInside;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.IsOutOfBounds}!");
                    return false;
            }
        }
    }
}
