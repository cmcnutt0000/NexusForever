using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the player's personal warplot rating with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049DD70 in the client.
    /// TODO: NexusForever doesn't track PvP ratings, they are 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Warplots255)]
    public class PrerequisiteCheckWarplots255 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckWarplots255> log;

        public PrerequisiteCheckWarplots255(
            ILogger<PrerequisiteCheckWarplots255> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Warplots255}!");

            return result;
        }
    }
}
