using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the creature's Creature2Difficulty with objectId.
    /// </summary>
    /// <remarks>
    /// See 0x14049EEB0 in the client (unit vtable +0x10, unit+0x30 with the creature's difficulty as fallback).
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.CreatureDifficulty)]
    public class PrerequisiteCheckCreatureDifficulty : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckCreatureDifficulty> log;

        public PrerequisiteCheckCreatureDifficulty(
            ILogger<PrerequisiteCheckCreatureDifficulty> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject.CreatureInfo == null)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, subject.CreatureInfo.Entry.Creature2DifficultyId, objectId, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.CreatureDifficulty}!");

            return result;
        }
    }
}
