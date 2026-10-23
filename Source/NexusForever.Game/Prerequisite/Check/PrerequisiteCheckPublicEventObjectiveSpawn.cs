using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit was spawned by the public event objective in objectId.
    /// </summary>
    /// <remarks>
    /// See 0x1404A08F0 in the client (unit+0x3780).
    /// TODO: NexusForever units don't know which public event spawned them.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PublicEventObjectiveSpawn)]
    public class PrerequisiteCheckPublicEventObjectiveSpawn : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPublicEventObjectiveSpawn> log;

        public PrerequisiteCheckPublicEventObjectiveSpawn(
            ILogger<PrerequisiteCheckPublicEventObjectiveSpawn> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PublicEventObjectiveSpawn}!");

            return result;
        }
    }
}
