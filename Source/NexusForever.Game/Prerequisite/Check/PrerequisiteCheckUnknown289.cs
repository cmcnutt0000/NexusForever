using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks bit 0 of an account flag field.
    /// </summary>
    /// <remarks>
    /// See 0x14049F0C0 in the client, which isn't identified. NexusForever has no equivalent, so it is clear.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Unknown289)]
    public class PrerequisiteCheckUnknown289 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnknown289> log;

        public PrerequisiteCheckUnknown289(
            ILogger<PrerequisiteCheckUnknown289> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Unknown289}!");

            return result;
        }
    }
}
