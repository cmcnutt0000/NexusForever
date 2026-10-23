using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Map;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the explored percentage of the map zone in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049E6B0 in the client, an unknown zone only passes <see cref="PrerequisiteComparison.NotEqual"/>.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ZoneExplored)]
    public class PrerequisiteCheckZoneExplored : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckZoneExplored> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckZoneExplored(
            ILogger<PrerequisiteCheckZoneExplored> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (gameTableManager.MapZone.GetEntry(objectId) == null)
                return comparison == PrerequisiteComparison.NotEqual;

            IZoneMap zoneMap = (subject as IPlayer)?.ZoneMapManager.GetZoneMap((ushort)objectId);
            float percent = zoneMap != null && zoneMap.MaxHexCount != 0u ? zoneMap.ExploredHexCount * 100f / zoneMap.MaxHexCount : 0f;

            if (!PrerequisiteComparer.TryCompare(comparison, percent, (float)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ZoneExplored}!");

            return result;
        }
    }
}
