using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the residence of the target housing plot.
    /// </summary>
    /// <remarks>
    /// See 0x1404A1070 in the client, the arguments of the residence comparison aren't recovered.
    /// TODO: NexusForever plugs don't know their residence.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.HouseOwnership)]
    public class PrerequisiteCheckHouseOwnership : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckHouseOwnership> log;

        public PrerequisiteCheckHouseOwnership(
            ILogger<PrerequisiteCheckHouseOwnership> log)
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
