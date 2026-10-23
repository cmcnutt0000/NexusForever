using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the wave of the holdout in objectId.
    /// </summary>
    /// <remarks>
    /// Server-only, the client has no implementation and always fails it.
    /// TODO: NexusForever has no holdouts. It fails as the client does.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HoldoutWave)]
    public class PrerequisiteCheckHoldoutWave : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHoldoutWave> log;

        public PrerequisiteCheckHoldoutWave(
            ILogger<PrerequisiteCheckHoldoutWave> log)
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
