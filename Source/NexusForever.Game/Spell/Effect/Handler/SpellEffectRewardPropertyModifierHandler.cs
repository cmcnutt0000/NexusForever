using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.RewardPropertyModifier)]
    public class SpellEffectRewardPropertyModifierHandler : ISpellEffectApplyHandler<ISpellEffectRewardPropertyModifierData>, ISpellEffectRemoveHandler<ISpellEffectRewardPropertyModifierData>
    {
        #region Dependency Injection

        private readonly IGameTableManager gameTableManager;

        public SpellEffectRewardPropertyModifierHandler(
            IGameTableManager gameTableManager)
        {
            this.gameTableManager = gameTableManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectRewardPropertyModifierData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            RewardPropertyEntry entry = gameTableManager.RewardProperty.GetEntry((ulong)data.Property);
            if (entry == null)
                return SpellEffectExecutionResult.Ok;

            // the value adds to the property and data like the premium modifiers do (+0.5 is "+50% XP")
            // TODO: nothing reads the XP, currency, reward track, OmniBits or reputation properties when granting them yet
            player.Account.RewardPropertyManager.UpdateRewardProperty(entry, data.Value, data.Data);
            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectRewardPropertyModifierData data)
        {
            if (target is not IPlayer player)
                return;

            RewardPropertyEntry entry = gameTableManager.RewardProperty.GetEntry((ulong)data.Property);
            if (entry == null)
                return;

            player.Account.RewardPropertyManager.UpdateRewardProperty(entry, -data.Value, data.Data);
        }
    }
}
