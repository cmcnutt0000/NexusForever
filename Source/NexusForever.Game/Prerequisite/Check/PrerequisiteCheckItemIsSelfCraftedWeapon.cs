using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Passes for <see cref="PrerequisiteComparison.Equal"/> and fails everything else.
    /// </summary>
    /// <remarks>
    /// See 0x1404A2100 (case 145) in the client, which has no check.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ItemIsSelfCraftedWeapon)]
    public class PrerequisiteCheckItemIsSelfCraftedWeapon : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItemIsSelfCraftedWeapon> log;

        public PrerequisiteCheckItemIsSelfCraftedWeapon(
            ILogger<PrerequisiteCheckItemIsSelfCraftedWeapon> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, true, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.ItemIsSelfCraftedWeapon}!");

            return result;
        }
    }
}
