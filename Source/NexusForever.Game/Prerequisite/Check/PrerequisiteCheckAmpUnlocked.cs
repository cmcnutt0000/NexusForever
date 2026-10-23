using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the AMP in objectId is unlocked for the player's current spec.
    /// </summary>
    /// <remarks>
    /// See 0x14049D760 in the client.
    /// TODO: NexusForever doesn't track unlocked AMPs, an equipped AMP counts as unlocked.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.AmpUnlocked)]
    public class PrerequisiteCheckAmpUnlocked : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckAmpUnlocked> log;

        public PrerequisiteCheckAmpUnlocked(
            ILogger<PrerequisiteCheckAmpUnlocked> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, player.SpellManager.GetActionSet(player.SpellManager.ActiveActionSet).GetAmp((ushort)objectId) != null, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.AmpUnlocked}!");

            return result;
        }
    }
}
