using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the AMP in objectId is equipped in the player's current spec.
    /// </summary>
    /// <remarks>
    /// See 0x14049D7B0 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.AmpEquipped)]
    public class PrerequisiteCheckAmpEquipped : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckAmpEquipped> log;

        public PrerequisiteCheckAmpEquipped(
            ILogger<PrerequisiteCheckAmpEquipped> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, player.SpellManager.GetActionSet(player.SpellManager.ActiveActionSet).GetAmp((ushort)objectId) != null, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.AmpEquipped}!");

            return result;
        }
    }
}
