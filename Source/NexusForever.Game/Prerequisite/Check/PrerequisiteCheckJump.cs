using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity.Movement.Command.State;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit's movement state has StateFlags.Jump.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 86) in the client (unit+0x1070 &amp; 0x40).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Jump)]
    public class PrerequisiteCheckJump : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckJump> log;

        public PrerequisiteCheckJump(
            ILogger<PrerequisiteCheckJump> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool hasFlag = ((subject.MovementManager?.GetState() ?? StateFlags.None) & StateFlags.Jump) != 0;

            if (!PrerequisiteComparer.TryEvaluate(comparison, hasFlag, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Jump}!");

            return result;
        }
    }
}
