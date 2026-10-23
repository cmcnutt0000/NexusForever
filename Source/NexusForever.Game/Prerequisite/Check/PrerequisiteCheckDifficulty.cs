using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.Game.Static.Setting;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the world difficulty against value.
    /// </summary>
    /// <remarks>
    /// See 0x14049EF70 in the client, 0 is normal and 1 veteran.
    /// TODO: NexusForever maps have no difficulty, they are all normal.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Difficulty)]
    public class PrerequisiteCheckDifficulty : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckDifficulty> log;

        public PrerequisiteCheckDifficulty(
            ILogger<PrerequisiteCheckDifficulty> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, (uint)WorldDifficulty.Normal == value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Difficulty}!");

            return result;
        }
    }
}
