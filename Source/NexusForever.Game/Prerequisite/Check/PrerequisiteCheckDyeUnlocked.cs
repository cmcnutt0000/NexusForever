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
    /// Checks if the dye colour ramp in objectId is unlocked.
    /// </summary>
    /// <remarks>
    /// See 0x1403C16E0 in the client, an unlocked ramp with its own prerequisite also needs that prerequisite to be met.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.DyeUnlocked)]
    public class PrerequisiteCheckDyeUnlocked : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckDyeUnlocked> log;
        private readonly IGameTableManager gameTableManager;
        private readonly IPrerequisiteManager prerequisiteManager;

        public PrerequisiteCheckDyeUnlocked(
            ILogger<PrerequisiteCheckDyeUnlocked> log,
            IGameTableManager gameTableManager,
            IPrerequisiteManager prerequisiteManager)
        {
            this.log                 = log;
            this.gameTableManager    = gameTableManager;
            this.prerequisiteManager = prerequisiteManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // only players and ghosts pass
            if (subject is not IPlayer player || subject.Type is not EntityType.Player and not EntityType.Ghost)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, IsUnlocked(player, objectId), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.DyeUnlocked}!");

            return result;
        }

        private bool IsUnlocked(IPlayer player, uint dyeColourRampId)
        {
            DyeColorRampEntry entry = gameTableManager.DyeColorRamp.GetEntry(dyeColourRampId);
            if (entry == null || !player.Account.GenericUnlockManager.IsDyeUnlocked(dyeColourRampId))
                return false;

            return entry.PrerequisiteId == 0u || prerequisiteManager.Meets(player, entry.PrerequisiteId);
        }
    }
}
