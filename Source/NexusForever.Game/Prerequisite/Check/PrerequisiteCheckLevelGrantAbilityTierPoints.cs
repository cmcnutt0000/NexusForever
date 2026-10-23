using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if reaching the unit's level grants ability tier points.
    /// </summary>
    /// <remarks>
    /// See 0x14049EFC0 in the client, which uses the level of the prerequisite's context. An unknown level fails every comparison.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.LevelGrantAbilityTierPoints)]
    public class PrerequisiteCheckLevelGrantAbilityTierPoints : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckLevelGrantAbilityTierPoints> log;
        private readonly IGameTableManager gameTableManager;

        public PrerequisiteCheckLevelGrantAbilityTierPoints(
            ILogger<PrerequisiteCheckLevelGrantAbilityTierPoints> log,
            IGameTableManager gameTableManager)
        {
            this.log              = log;
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            XpPerLevelEntry entry = gameTableManager.XpPerLevel.GetEntry(subject.Level);
            if (entry == null)
                return false;

            if (!PrerequisiteComparer.TryEvaluate(comparison, entry.AbilityPointsPerLevel != 0u, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.LevelGrantAbilityTierPoints}!");

            return result;
        }
    }
}
