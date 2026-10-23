using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the state of the episode in objectId against value.
    /// </summary>
    /// <remarks>
    /// See 0x14049F700 in the client, an unknown episode fails <see cref="PrerequisiteComparison.Equal"/>.
    /// TODO: NexusForever doesn't track or send episodes.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.EpisodeState)]
    public class PrerequisiteCheckEpisodeState : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckEpisodeState> log;

        public PrerequisiteCheckEpisodeState(
            ILogger<PrerequisiteCheckEpisodeState> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.EpisodeState}!");

            return result;
        }
    }
}
