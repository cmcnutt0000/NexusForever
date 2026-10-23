using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the challenge in objectId is available.
    /// </summary>
    /// <remarks>
    /// See 0x14049F990 in the client, an unknown challenge fails <see cref="PrerequisiteComparison.Equal"/>.
    /// TODO: NexusForever has no challenges, every challenge is unknown.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ChallengeRequirement)]
    public class PrerequisiteCheckChallengeRequirement : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckChallengeRequirement> log;

        public PrerequisiteCheckChallengeRequirement(
            ILogger<PrerequisiteCheckChallengeRequirement> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ChallengeRequirement}!");

            return result;
        }
    }
}
