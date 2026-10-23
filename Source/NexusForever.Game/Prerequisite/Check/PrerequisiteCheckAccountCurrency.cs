using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Account;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    /// <summary>
    /// Compares the account's amount of the <see cref="AccountCurrencyType"/> in objectId with value.
    /// </summary>
    /// <remarks>
    /// See 0x14049D3A0 in the client.
    /// </remarks>
    [PrerequisiteCheck(PrerequisiteType.AccountCurrency)]
    public class PrerequisiteCheckAccountCurrency : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly ILogger<PrerequisiteCheckAccountCurrency> log;

        public PrerequisiteCheckAccountCurrency(
            ILogger<PrerequisiteCheckAccountCurrency> log)
        {
            this.log = log;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            if (subject is not IPlayer player)
                return false;

            if (!PrerequisiteComparer.TryCompare(comparison, player.Account.CurrencyManager.GetAmount((AccountCurrencyType)objectId), (ulong)value, out bool result))
                log.LogWarning($"Unhandled PrerequisiteComparison {comparison} for {PrerequisiteType.AccountCurrency}!");

            return result;
        }
    }
}
