using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit can afford the next upgrade of the target warplot plug.
    /// </summary>
    /// <remarks>
    /// See 0x1404A1370 in the client (HousingWarplotPlugInfo and GameFormula 867).
    /// TODO: NexusForever plugs don't track their upgrade tier, so the target isn't an upgradable plug.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.WarplotUpgrade)]
    public class PrerequisiteCheckWarplotUpgrade : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckWarplotUpgrade> log;

        public PrerequisiteCheckWarplotUpgrade(
            ILogger<PrerequisiteCheckWarplotUpgrade> log)
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
