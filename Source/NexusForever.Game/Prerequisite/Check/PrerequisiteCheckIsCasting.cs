using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is casting a spell.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 191) in the client (unit+0x1600, same as Lua Unit:IsCasting).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.IsCasting)]
    public class PrerequisiteCheckIsCasting : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckIsCasting> log;

        public PrerequisiteCheckIsCasting(
            ILogger<PrerequisiteCheckIsCasting> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, subject.IsCasting(), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.IsCasting}!");

            return result;
        }
    }
}
