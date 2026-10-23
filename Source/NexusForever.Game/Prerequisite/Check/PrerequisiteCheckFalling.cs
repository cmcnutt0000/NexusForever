using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Entity.Movement.Command.State;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit's movement state has the falling bit.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 281) in the client, which tests 0x4 rather than <see cref="StateFlags.Fall"/>.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Falling)]
    public class PrerequisiteCheckFalling : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckFalling> log;

        public PrerequisiteCheckFalling(
            ILogger<PrerequisiteCheckFalling> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isFalling = ((uint)(subject.MovementManager?.GetState() ?? StateFlags.None) & 0x4u) != 0;

            if (!PrerequisiteComparer.TryEvaluate(comparison, isFalling, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Falling}!");

            return result;
        }
    }
}
