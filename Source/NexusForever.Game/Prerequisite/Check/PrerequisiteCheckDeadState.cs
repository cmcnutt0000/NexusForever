using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the unit's death state with value, 0 is alive and 1 is dead.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 12) in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.DeadState)]
    public class PrerequisiteCheckDeadState : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckDeadState> log;

        public PrerequisiteCheckDeadState(
            ILogger<PrerequisiteCheckDeadState> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            uint state = subject.IsAlive ? 0u : 1u;
            bool matches = state == value;

            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return matches;
                case PrerequisiteComparison.NotEqual:
                    return !matches;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.DeadState}!");
                    return false;
            }
        }
    }
}
