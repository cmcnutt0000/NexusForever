using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is in combat, as 0 or 1, against value.
    /// </summary>
    /// <remarks>
    /// See 0x14049C840 in the client, which also counts a unit flag at unit+0x1E4 set by a server message that isn't identified yet.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.State)]
    public class PrerequisiteCheckState : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckState> log;

        public PrerequisiteCheckState(
            ILogger<PrerequisiteCheckState> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, (subject.InCombat ? 1u : 0u) == value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.State}!");

            return result;
        }
    }
}
