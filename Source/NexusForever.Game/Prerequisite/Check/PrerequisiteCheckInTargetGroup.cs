using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is a member of the target group in value.
    /// </summary>
    /// <remarks>
    /// See 0x14049EE40 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.InTargetGroup)]
    public class PrerequisiteCheckInTargetGroup : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckInTargetGroup> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckInTargetGroup(
            ILogger<PrerequisiteCheckInTargetGroup> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (value == 0u)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, subject.IsInTargetGroup(gameTableManager, value), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.InTargetGroup}!");

            return result;
        }
    }
}
