using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <summary>
    /// Shows an ItemDisplay in one of the target's item slots while the effect is active.
    /// </summary>
    /// <remarks>
    /// The client has its own class for the effect: it saves the slot's visual and swaps in the display (0x1405D9BE0) and puts the saved one back when the effect goes (0x1405D9B60).
    /// The server keeps the swap so the visual updates it sends while the effect is active show it as well, see <see cref="SpellEffectVisualOverlay"/>, a visual update otherwise replaces the swapped display.
    /// 1,495 of the 1,542 rows are CancelOnly and last until the aura goes, 42 have a duration.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.ItemVisualSwap)]
    public class SpellEffectItemVisualSwapHandler : SpellEffectCombatEffectHandler<ISpellEffectItemVisualSwapData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectItemVisualSwapData data)
        {
            // the client does nothing for equipment slots it doesn't map to an item slot
            if (data.Slot == null)
                return SpellEffectExecutionResult.Ok;

            base.Apply(executionContext, target, info, data);
            target.SetVisualEmit(true);
            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectItemVisualSwapData data)
        {
            if (data.Slot == null)
                return;

            base.Remove(spell, target, info, data);
            target.SetVisualEmit(true);
        }
    }
}
