using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Abstract.Reputation;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Reputation;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit's faction, or a parent of it, has a relationship with the faction in value.
    /// </summary>
    /// <remarks>
    /// See 0x14049C720 in the client, relationships are linked to both of their factions.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.FactionRelationship)]
    public class PrerequisiteCheckFactionRelationship : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckFactionRelationship> log;
        private readonly IFactionManager factionManager;

        public PrerequisiteCheckFactionRelationship(
            ILogger<PrerequisiteCheckFactionRelationship> log,
            IFactionManager factionManager)
        {
            this.log            = log;
            this.factionManager = factionManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, HasRelationship(subject, (Faction)value), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.FactionRelationship}!");

            return result;
        }

        private bool HasRelationship(IUnitEntity unit, Faction factionId)
        {
            IFactionNode target = factionManager.GetFaction(factionId);
            for (IFactionNode node = factionManager.GetFaction(unit.Faction1); node != null; node = node.Parent)
            {
                if (node.GetRelationships().Any(r => r.Id == factionId))
                    return true;
                if (target != null && target.GetRelationships().Any(r => r.Id == node.FactionId))
                    return true;
            }

            return false;
        }
    }
}
