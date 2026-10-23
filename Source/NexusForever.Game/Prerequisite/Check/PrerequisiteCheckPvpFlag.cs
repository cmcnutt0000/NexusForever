using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.PVP;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is flagged for PvP, as 0 or 1, against value.
    /// </summary>
    /// <remarks>
    /// See 0x1403B48B0 in the client, a mount or vehicle uses the flag of its owner or pilot.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PvpFlag)]
    public class PrerequisiteCheckPvpFlag : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPvpFlag> log;

        public PrerequisiteCheckPvpFlag(
            ILogger<PrerequisiteCheckPvpFlag> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, GetPvpFlag(subject) == value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PvpFlag}!");

            return result;
        }

        private static uint GetPvpFlag(IWorldEntity entity)
        {
            switch (entity)
            {
                case IPlayer player:
                    return (player.PvPFlags & PvPFlag.Enabled) != 0 ? 1u : 0u;
                case IMountEntity mount:
                    return mount.GetVisible<IWorldEntity>(mount.OwnerGuid) is IWorldEntity owner && owner != entity ? GetPvpFlag(owner) : 0u;
                case IVehicleEntity vehicle:
                    IVehiclePassenger pilot = vehicle.FirstOrDefault(p => p.SeatType == VehicleSeatType.Pilot);
                    return pilot != null && vehicle.GetVisible<IWorldEntity>(pilot.Guid) is IWorldEntity driver && driver != entity ? GetPvpFlag(driver) : 0u;
                default:
                    return 0u;
            }
        }
    }
}
