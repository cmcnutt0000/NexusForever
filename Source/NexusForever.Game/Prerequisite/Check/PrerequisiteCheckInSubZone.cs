using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is in the WorldZone in value or in a zone inside it.
    /// </summary>
    /// <remarks>
    /// See 0x1403C92E0 in the client, which walks up ParentZoneId from the current zone.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.InSubZone)]
    public class PrerequisiteCheckInSubZone : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckInSubZone> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckInSubZone(
            ILogger<PrerequisiteCheckInSubZone> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool inZone = IsInZone(subject.Zone, value);

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return inZone;
                case PrerequisiteComparison.NotEqual:
                    return !inZone;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.InSubZone}!");
                    return false;
            }
        }

        private bool IsInZone(WorldZoneEntry zone, uint worldZoneId)
        {
            // limit the walk in case the data ever has a loop
            for (int depth = 0; zone != null && depth < 32; depth++)
            {
                if (zone.Id == worldZoneId)
                    return true;

                zone = zone.ParentZoneId != 0u ? gameTableManager.WorldZone.GetEntry(zone.ParentZoneId) : null;
            }

            return false;
        }
    }
}
