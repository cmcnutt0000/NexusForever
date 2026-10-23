using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the player's bonus AMP power with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049C990 in the client, only players and ghosts pass.
    /// TODO: NexusForever doesn't track bonus AMP power.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.BonusPower)]
    public class PrerequisiteCheckBonusPower : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckBonusPower> log;

        public PrerequisiteCheckBonusPower(
            ILogger<PrerequisiteCheckBonusPower> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // only players and ghosts pass
            if (subject.Type is not EntityType.Player and not EntityType.Ghost)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.BonusPower}!");

            return result;
        }
    }
}
