using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player has completed the datacube volume in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x1404A15D0 in the client, a volume is complete when every page with a datacube has its progress bit set.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Volume)]
    public class PrerequisiteCheckVolume : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckVolume> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckVolume(
            ILogger<PrerequisiteCheckVolume> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // only players and ghosts pass
            if (subject is not IPlayer player || subject.Type is not EntityType.Player and not EntityType.Ghost)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, IsComplete(player, objectId), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Volume}!");

            return result;
        }

        private bool IsComplete(IPlayer player, uint volumeId)
        {
            DatacubeVolumeEntry entry = gameTableManager.DatacubeVolume.GetEntry(volumeId);
            IDatacube volume = player.DatacubeManager.GetDatacube((ushort)volumeId, DatacubeType.Journal);
            if (entry == null || volume == null)
                return false;

            uint[] pages =
            [
                entry.DatacubeId00, entry.DatacubeId01, entry.DatacubeId02, entry.DatacubeId03,
                entry.DatacubeId04, entry.DatacubeId05, entry.DatacubeId06, entry.DatacubeId07,
                entry.DatacubeId08, entry.DatacubeId09, entry.DatacubeId10, entry.DatacubeId11,
                entry.DatacubeId12, entry.DatacubeId13, entry.DatacubeId14, entry.DatacubeId15
            ];

            for (int i = 0; i < pages.Length; i++)
                if (pages[i] != 0u && (volume.Progress & (1u << i)) == 0u)
                    return false;

            return true;
        }
    }
}
