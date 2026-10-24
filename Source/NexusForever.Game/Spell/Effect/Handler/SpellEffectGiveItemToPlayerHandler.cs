using NexusForever.Game.Abstract;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Network.World.Message.Static;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.GiveItemToPlayer)]
    public class SpellEffectGiveItemToPlayerHandler : ISpellEffectApplyHandler<ISpellEffectGiveItemToPlayerData>
    {
        #region Dependency Injection

        private readonly IItemManager itemManager;

        public SpellEffectGiveItemToPlayerHandler(
            IItemManager itemManager)
        {
            this.itemManager = itemManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectGiveItemToPlayerData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            // 5 effects name items that are no longer in Item2
            IItemInfo itemInfo = itemManager.GetItemInfo(data.ItemId);
            if (itemInfo == null)
                return SpellEffectExecutionResult.Ok;

            player.Inventory.ItemCreate(InventoryLocation.Inventory, itemInfo, data.Count, ItemUpdateReason.SpellEffect);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
