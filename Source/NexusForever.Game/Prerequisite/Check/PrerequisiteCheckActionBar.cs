using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player's action bar in value (0-3) is enabled.
    /// </summary>
    /// <remarks>
    /// See 0x14049CA10 in the client, only players and ghosts pass.
    /// TODO: NexusForever doesn't track which action bars are enabled.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ActionBar)]
    public class PrerequisiteCheckActionBar : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckActionBar> log;

        public PrerequisiteCheckActionBar(
            ILogger<PrerequisiteCheckActionBar> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject.Type is not EntityType.Player and not EntityType.Ghost)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ActionBar}!");

            return result;
        }
    }
}
