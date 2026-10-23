using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares a field of the challenge in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049FBE0 in the client, an unknown challenge counts as 0.
    /// TODO: NexusForever has no challenges, every challenge is unknown.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ChallengeValue)]
    public class PrerequisiteCheckChallengeValue : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckChallengeValue> log;

        public PrerequisiteCheckChallengeValue(
            ILogger<PrerequisiteCheckChallengeValue> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ChallengeValue}!");

            return result;
        }
    }
}
