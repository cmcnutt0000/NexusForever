using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks if the unit is one of the target's pets.
    /// </summary>
    /// <remarks>
    /// See 0x14049D470 in the client (target+0x2B8), with no target only <see cref="PrerequisiteComparison.NotEqual"/> passes.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Pet)]
    public class PrerequisiteCheckPet : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckPet> log;

        public PrerequisiteCheckPet(
            ILogger<PrerequisiteCheckPet> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            IUnitEntity target = parameters?.Target;
            if (target == null)
                return comparison == PrerequisiteComparison.NotEqual;

            bool isPet = subject.SummonerGuid == target.Guid || (target is IPlayer player && player.VanityPetGuid == subject.Guid);

            if (!PrerequisiteComparer.TryEvaluate(comparison, isPet, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Pet}!");

            return result;
        }
    }
}
