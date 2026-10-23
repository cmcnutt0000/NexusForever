using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks <see cref="PrerequisiteType.PositionalRequirement"/> with the unit and the target swapped.
    /// </summary>
    /// <remarks>
    /// See 0x14049D940 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PositionalRequirementTarget)]
    public class PrerequisiteCheckPositionalRequirementTarget : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPositionalRequirementTarget> log;
        private readonly IPrerequisiteCheck positionalRequirement;

        public PrerequisiteCheckPositionalRequirementTarget(
            ILogger<PrerequisiteCheckPositionalRequirementTarget> log,
            [FromKeyedServices(PrerequisiteType.PositionalRequirement)] IPrerequisiteCheck positionalRequirement)
        {
            this.log                   = log;
            this.positionalRequirement = positionalRequirement;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (parameters?.Target == null)
                return false;

            var swapped = new PrerequisiteParameters
            {
                CastResult = parameters.CastResult,
                Target     = subject,
                TaxiNode   = parameters.TaxiNode
            };

            return positionalRequirement.Meets(parameters.Target, comparison, value, objectId, swapped);
        }
    }
}
