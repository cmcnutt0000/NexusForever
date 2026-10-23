using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks a flag of the challenge in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x14049FAC0 in the client, an unknown challenge fails <see cref="PrerequisiteComparison.Equal"/>.
    /// TODO: NexusForever has no challenges, every challenge is unknown.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ChallengeFlag)]
    public class PrerequisiteCheckChallengeFlag : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckChallengeFlag> log;

        public PrerequisiteCheckChallengeFlag(
            ILogger<PrerequisiteCheckChallengeFlag> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ChallengeFlag}!");

            return result;
        }
    }
}
