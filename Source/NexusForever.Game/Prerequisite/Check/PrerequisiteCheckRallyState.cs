using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the player's rally or mentor state with objectId.
    /// </summary>
    /// <remarks>
    /// See 0x14049E7E0 in the client (same object as Lua Unit:IsRallied and IsMentoring).
    /// TODO: NexusForever has no mentoring or rallying, the state is always 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.RallyState)]
    public class PrerequisiteCheckRallyState : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckRallyState> log;

        public PrerequisiteCheckRallyState(
            ILogger<PrerequisiteCheckRallyState> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, objectId == 0u, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.RallyState}!");

            return result;
        }
    }
}
