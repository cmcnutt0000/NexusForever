using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the target housing plot belongs to the player or its plug flags allow use.
    /// </summary>
    /// <remarks>
    /// See 0x1404A1220 in the client.
    /// TODO: NexusForever plugs don't know their residence.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Unknown260)]
    public class PrerequisiteCheckUnknown260 : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckUnknown260> log;

        public PrerequisiteCheckUnknown260(
            ILogger<PrerequisiteCheckUnknown260> log)
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
