using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player knows the spell base of the spell in objectId at the spell's tier or higher.
    /// </summary>
    /// <remarks>
    /// See 0x14049D820 in the client, the tier of the current spec counts and an unknown spell fails every comparison.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.SpellTierKnown)]
    public class PrerequisiteCheckSpellTierKnown : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckSpellTierKnown> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckSpellTierKnown(
            ILogger<PrerequisiteCheckSpellTierKnown> log,
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

            Spell4Entry entry = gameTableManager.Spell4.GetEntry(objectId);
            if (entry == null || player.SpellManager.GetSpell(entry.Spell4BaseIdBaseSpell) == null)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, player.SpellManager.GetSpellTier(entry.Spell4BaseIdBaseSpell) >= entry.TierIndex, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.SpellTierKnown}!");

            return result;
        }
    }
}
