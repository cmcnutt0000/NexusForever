using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is an objective unit of the challenge in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x14049FC70 in the client.
    /// TODO: NexusForever has no challenges, every challenge is unknown.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ChallengeObject)]
    public class PrerequisiteCheckChallengeObject : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckChallengeObject> log;

        public PrerequisiteCheckChallengeObject(
            ILogger<PrerequisiteCheckChallengeObject> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // the non-player unit is the subject, or the target when the subject is a player
            IUnitEntity unit = subject is IPlayer ? parameters?.Target : subject;
            if (unit == null || unit is IPlayer)
                return comparison == PrerequisiteComparison.NotEqual;

            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ChallengeObject}!");

            return result;
        }
    }
}
