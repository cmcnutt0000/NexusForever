using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Checks the unit's race row against value.
    /// </summary>
    /// <remarks>
    /// See 0x14049C520 in the client (unit+0xD0), a UnitRace id for creatures; for players the Race id is assumed.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.Racial)]
    public class PrerequisiteCheckRacial : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckRacial> log;

        public PrerequisiteCheckRacial(
            ILogger<PrerequisiteCheckRacial> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (!PrerequisiteComparer.TryEvaluate(comparison, subject is IPlayer player ? (uint)player.Race == value : subject.CreatureInfo?.Entry.UnitRaceId == value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.Racial}!");

            return result;
        }
    }
}
