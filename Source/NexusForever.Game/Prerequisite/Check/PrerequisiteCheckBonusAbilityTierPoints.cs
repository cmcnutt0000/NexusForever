using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the player's bonus ability tier points with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049C910 in the client, only players and ghosts pass.
    /// TODO: NexusForever doesn't track bonus ability tier points.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.BonusAbilityTierPoints)]
    public class PrerequisiteCheckBonusAbilityTierPoints : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckBonusAbilityTierPoints> log;

        public PrerequisiteCheckBonusAbilityTierPoints(
            ILogger<PrerequisiteCheckBonusAbilityTierPoints> log)
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
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.BonusAbilityTierPoints}!");

            return result;
        }
    }
}
