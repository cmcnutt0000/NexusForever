using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player has a bind point.
    /// </summary>
    /// <remarks>
    /// See 0x14049CA70 in the client: false for anything but a player, otherwise whether the current bind point (the field
    /// GameLib.HasBindPoint and Unit:IsCurrentBindPoint read) is a BindPoint entry. The client also accepts a player's ghost
    /// (unit type 23), which isn't an <see cref="IPlayer"/> here.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HasBindPoint)]
    public class PrerequisiteCheckHasBindPoint : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHasBindPoint> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckHasBindPoint(
            ILogger<PrerequisiteCheckHasBindPoint> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            bool hasBindPoint = gameTableManager.BindPoint.GetEntry(player.BindPoint) != null;
            if (!PrerequisiteComparer.TryEvaluate(comparison, hasBindPoint, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.HasBindPoint}!");

            return result;
        }
    }
}
