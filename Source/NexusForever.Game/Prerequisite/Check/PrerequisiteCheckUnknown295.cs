using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Passes for <see cref="PrerequisiteComparison.NotEqual"/> and fails everything else.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 295) in the client, which has no check.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Unknown295)]
    public class PrerequisiteCheckUnknown295 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnknown295> log;

        public PrerequisiteCheckUnknown295(
            ILogger<PrerequisiteCheckUnknown295> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Unknown295}!");

            return result;
        }
    }
}
