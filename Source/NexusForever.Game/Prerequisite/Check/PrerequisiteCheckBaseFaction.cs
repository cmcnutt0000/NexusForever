using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Reputation;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the unit's base faction, which a temporary faction doesn't replace, against value.
    /// </summary>
    /// <remarks>
    /// See 0x14049C6A0 in the client (unit+0x110), the client sets it from Faction2 of ServerEntityCreate.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.BaseFaction)]
    public class PrerequisiteCheckBaseFaction : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckBaseFaction> log;

        public PrerequisiteCheckBaseFaction(
            ILogger<PrerequisiteCheckBaseFaction> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    return subject.Faction2 == (Faction)value;
                case PrerequisiteComparison.NotEqual:
                    return subject.Faction2 != (Faction)value;
                default:
                    log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.BaseFaction}!");
                    return false;
            }
        }
    }
}
