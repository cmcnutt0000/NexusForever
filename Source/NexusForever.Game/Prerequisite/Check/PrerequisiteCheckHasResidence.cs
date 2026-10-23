using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player owns a residence.
    /// </summary>
    /// <remarks>
    /// See 0x1404A11E0 in the client, only players and ghosts pass ("You must own a house to use Report Home").
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HasResidence)]
    public class PrerequisiteCheckHasResidence : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHasResidence> log;

        public PrerequisiteCheckHasResidence(
            ILogger<PrerequisiteCheckHasResidence> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, player.ResidenceManager.Residence != null, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.HasResidence}!");

            return result;
        }
    }
}
