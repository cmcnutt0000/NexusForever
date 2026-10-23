using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a spell effect in the effect group in objectId, from any caster, is active on the unit.
    /// </summary>
    /// <remarks>
    /// See 0x14046A210 in the client, an effect is in the groups of its Spell4EffectGroupList up to the first empty one (0x14055E180).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.UnderSpellEffectGroup)]
    public class PrerequisiteCheckUnderSpellEffectGroup : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnderSpellEffectGroup> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckUnderSpellEffectGroup(
            ILogger<PrerequisiteCheckUnderSpellEffectGroup> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isAffected = objectId != 0u && subject.IsAffectedBySpellEffect(e => IsInGroup(e, objectId));

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return isAffected;
                case PrerequisiteComparison.NotEqual:
                    return !isAffected;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.UnderSpellEffectGroup}!");
                    return false;
            }
        }

        private bool IsInGroup(Spell4EffectsEntry entry, uint effectGroupId)
        {
            Spell4EffectGroupListEntry groupList = gameTableManager.Spell4EffectGroupList.GetEntry(entry.Spell4EffectGroupListId);
            return groupList != null && groupList.Spell4EffectGroupIds
                .TakeWhile(g => g != 0u)
                .Contains(effectGroupId);
        }
    }
}
