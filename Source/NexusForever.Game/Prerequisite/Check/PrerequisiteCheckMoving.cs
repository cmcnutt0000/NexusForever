using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity.Movement.Command.State;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit's movement state has StateFlags.Move.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 87) in the client (unit+0x1070 &amp; 0x2).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Moving)]
    public class PrerequisiteCheckMoving : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckMoving> log;

        public PrerequisiteCheckMoving(
            ILogger<PrerequisiteCheckMoving> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool hasFlag = ((subject.MovementManager?.GetState() ?? StateFlags.None) & StateFlags.Move) != 0;

            if (!PrerequisiteComparer.TryEvaluate(comparison, hasFlag, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Moving}!");

            return result;
        }
    }
}
