using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the player has a bind point.
    /// </summary>
    /// <remarks>
    /// See 0x14049CA70 in the client (same field as Lua Unit:IsCurrentBindPoint).
    /// TODO: NexusForever doesn't store bind points.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HasBindPoint)]
    public class PrerequisiteCheckHasBindPoint : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHasBindPoint> log;

        public PrerequisiteCheckHasBindPoint(
            ILogger<PrerequisiteCheckHasBindPoint> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.HasBindPoint}!");

            return result;
        }
    }
}
