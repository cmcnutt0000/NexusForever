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
    /// Checks if the unit is inside the trigger volume in value.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// Half of the values are WorldLocation2 ids, those use the location's radius and vertical distance.
    /// TODO: the other values are trigger volumes from the map data, which NexusForever doesn't load.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.InTriggerVolume)]
    public class PrerequisiteCheckInTriggerVolume : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckInTriggerVolume> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckInTriggerVolume(
            ILogger<PrerequisiteCheckInTriggerVolume> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            WorldLocation2Entry entry = gameTableManager.WorldLocation2.GetEntry(value);
            if (entry == null || subject.Map?.Entry?.Id != entry.WorldId)
                return comparison == PrerequisiteComparison.NotEqual;

            var position = new Vector3(entry.Position0, entry.Position1, entry.Position2);
            float horizontal = Vector2.Distance(new Vector2(subject.Position.X, subject.Position.Z), new Vector2(position.X, position.Z));
            bool isInside = horizontal <= entry.Radius
                && (entry.MaxVerticalDistance == 0f || MathF.Abs(subject.Position.Y - position.Y) <= entry.MaxVerticalDistance);

            if (!PrerequisiteComparer.TryEvaluate(comparison, isInside, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.InTriggerVolume}!");

            return result;
        }
    }
}
