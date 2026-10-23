using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks a permission of the target's residence.
    /// </summary>
    /// <remarks>
    /// See 0x1404A10E0 in the client.
    /// TODO: NexusForever plugs don't know their residence.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ResidencePermission)]
    public class PrerequisiteCheckResidencePermission : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckResidencePermission> log;

        public PrerequisiteCheckResidencePermission(
            ILogger<PrerequisiteCheckResidencePermission> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            return false;
        }
    }
}
