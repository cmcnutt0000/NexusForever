using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the size of the player's group, 0 when not grouped, with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049E820 in the client.
    /// TODO: the world server only knows the player's group id, member count, leader and flags live on the group server. A grouped player counts as a group of 2.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.GroupSize)]
    public class PrerequisiteCheckGroupSize : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckGroupSize> log;

        public PrerequisiteCheckGroupSize(
            ILogger<PrerequisiteCheckGroupSize> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            uint groupSize = subject is IPlayer player && player.GroupAssociation != 0ul ? 2u : 0u;

            if (!PrerequisiteComparer.TryCompare(comparison, groupSize, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.GroupSize}!");

            return result;
        }
    }
}
