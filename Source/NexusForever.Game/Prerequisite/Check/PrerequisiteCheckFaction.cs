using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Reputation;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the unit's current faction, which a temporary faction replaces, against value.
    /// </summary>
    /// <remarks>
    /// See 0x14049C620 in the client (unit+0x118), the client sets it from Faction1 of ServerEntityCreate and from ServerEntityFaction.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Faction)]
    public class PrerequisiteCheckFaction : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckFaction> log;

        public PrerequisiteCheckFaction(
            ILogger<PrerequisiteCheckFaction> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return subject.Faction1 == (Faction)value;
                case PrerequisiteComparison.NotEqual:
                    return subject.Faction1 != (Faction)value;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Faction}!");
                    return false;
            }
        }
    }
}
