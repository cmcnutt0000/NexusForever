using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks bit value of the player's phase mask.
    /// </summary>
    /// <remarks>
    /// See 0x14049E640 in the client (same function as CanSeePhase).
    /// TODO: NexusForever has no phases and always sends a phase mask of 1.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.InPhase)]
    public class PrerequisiteCheckInPhase : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckInPhase> log;

        public PrerequisiteCheckInPhase(
            ILogger<PrerequisiteCheckInPhase> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isInPhase = (1u >> (int)(value & 31u) & 1u) != 0;

            if (!PrerequisiteComparer.TryEvaluate(comparison, isInPhase, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.InPhase}!");

            return result;
        }
    }
}
