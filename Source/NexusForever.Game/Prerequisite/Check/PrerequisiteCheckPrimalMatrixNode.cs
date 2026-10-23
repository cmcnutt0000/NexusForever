using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the player's progress in the Primal Matrix node in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x1404A18F0 in the client ("Unlocked through Primal Matrix").
    /// TODO: NexusForever has no Primal Matrix, every node is 0.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.PrimalMatrixNode)]
    public class PrerequisiteCheckPrimalMatrixNode : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPrimalMatrixNode> log;

        public PrerequisiteCheckPrimalMatrixNode(
            ILogger<PrerequisiteCheckPrimalMatrixNode> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, 0u, value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.PrimalMatrixNode}!");

            return result;
        }
    }
}
