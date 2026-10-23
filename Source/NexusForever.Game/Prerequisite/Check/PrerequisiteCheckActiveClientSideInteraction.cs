using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if a client side interaction is running, and if value isn't 0 that it is the one with that id.
    /// </summary>
    /// <remarks>
    /// See 0x14049C880 in the client (same field as Lua GetActiveCSI and IsCSIRunning).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ActiveClientSideInteraction)]
    public class PrerequisiteCheckActiveClientSideInteraction : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckActiveClientSideInteraction> log;

        public PrerequisiteCheckActiveClientSideInteraction(
            ILogger<PrerequisiteCheckActiveClientSideInteraction> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            bool isRunning = subject.HasSpell(s => s.IsCasting && s.Parameters.ClientSideInteraction != null
                && (value == 0u || s.Parameters.SpellInfo.BaseInfo.Entry.ClientSideInteractionId == value), out _);

            if (!PrerequisiteComparer.TryEvaluate(comparison, isRunning, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ActiveClientSideInteraction}!");

            return result;
        }
    }
}
