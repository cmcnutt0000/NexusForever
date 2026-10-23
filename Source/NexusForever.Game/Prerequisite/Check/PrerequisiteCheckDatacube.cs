using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player has discovered the datacube in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x1404A1580 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Datacube)]
    public class PrerequisiteCheckDatacube : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckDatacube> log;

        public PrerequisiteCheckDatacube(
            ILogger<PrerequisiteCheckDatacube> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // only players and ghosts pass
            if (subject is not IPlayer player || subject.Type is not EntityType.Player and not EntityType.Ghost)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, player.DatacubeManager.GetDatacube((ushort)objectId, DatacubeType.Datacube) != null, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Datacube}!");

            return result;
        }
    }
}
