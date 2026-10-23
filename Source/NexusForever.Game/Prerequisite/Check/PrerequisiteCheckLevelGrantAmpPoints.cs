using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit's level is at least the level AMP points are granted from.
    /// </summary>
    /// <remarks>
    /// See 0x14049F010 in the client, GameFormula 942 Dataint0 with a default of 6.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.LevelGrantAmpPoints)]
    public class PrerequisiteCheckLevelGrantAmpPoints : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckLevelGrantAmpPoints> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckLevelGrantAmpPoints(
            ILogger<PrerequisiteCheckLevelGrantAmpPoints> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, subject.Level >= (gameTableManager.GameFormula.GetEntry(942)?.Dataint0 ?? 6u), out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.LevelGrantAmpPoints}!");

            return result;
        }
    }
}
