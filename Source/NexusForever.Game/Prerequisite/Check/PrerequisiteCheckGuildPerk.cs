using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is in the player's guild and guild perk bit value is unlocked.
    /// </summary>
    /// <remarks>
    /// See 0x14049EAE0 in the client, without a unit only <see cref="PrerequisiteComparison.NotEqual"/> passes.
    /// TODO: NexusForever doesn't track guild perks.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.GuildPerk)]
    public class PrerequisiteCheckGuildPerk : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckGuildPerk> log;

        public PrerequisiteCheckGuildPerk(
            ILogger<PrerequisiteCheckGuildPerk> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, false, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.GuildPerk}!");

            return result;
        }
    }
}
