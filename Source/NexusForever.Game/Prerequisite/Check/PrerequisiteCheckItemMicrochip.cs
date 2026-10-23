using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the microchip bit in value of the item being evaluated.
    /// </summary>
    /// <remarks>
    /// See 0x1404A0E10 in the client, used by achievement objectives.
    /// TODO: NexusForever doesn't pass the item being evaluated, the client fails every comparison without one.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.ItemMicrochip)]
    public class PrerequisiteCheckItemMicrochip : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckItemMicrochip> log;

        public PrerequisiteCheckItemMicrochip(
            ILogger<PrerequisiteCheckItemMicrochip> log)
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
