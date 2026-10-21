using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Prerequisite.Check
{
    [PrerequisiteCheck(PrerequisiteType.ItemProficiency)]
    public class PrerequisiteCheckItemProficiency : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItemProficiency> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckItemProficiency(
            ILogger<PrerequisiteCheckItemProficiency> log,
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

            // value is an ItemProficiency entry id rather than a mask, see 0x14049D160 in the client
            // the client treats a missing entry or an empty mask as proficient
            ItemProficiencyEntry entry = gameTableManager.ItemProficiency.GetEntry(value);
            bool hasProficiency = entry == null
                || entry.BitMask == 0u
                || (player.GetItemProficiencies() & (ItemProficiency)entry.BitMask) != 0;

            switch (comparison)
            {
                case PrerequisiteComparison.NotEqual:
                    return !hasProficiency;
                case PrerequisiteComparison.Equal:
                    return hasProficiency;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ItemProficiency}!");
                    return false;
            }
        }
    }
}
