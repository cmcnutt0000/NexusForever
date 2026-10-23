using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares an account field with objectId.
    /// </summary>
    /// <remarks>
    /// See 0x14049F090 in the client, a field that also disables CREDD and forces the profanity filter when it is 1.
    /// NexusForever has no equivalent, so it is 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Unknown268)]
    public class PrerequisiteCheckUnknown268 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnknown268> log;

        public PrerequisiteCheckUnknown268(
            ILogger<PrerequisiteCheckUnknown268> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, objectId == 0u, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Unknown268}!");

            return result;
        }
    }
}
