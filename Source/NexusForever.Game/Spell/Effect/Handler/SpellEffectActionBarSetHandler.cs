using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Network.World.Message.Model.Abilities;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <summary>
    /// Shows an action bar of ActionBarShortcutSet shortcuts to the target while the effect is active, eg: quest items, turrets and carried objects.
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120), the bar comes from <see cref="ServerShowActionBar"/>: the client fills the bar from the ActionBarShortcutSet row and clears it for id 0 (0x1403B9920).
    /// When the effect goes the bar of the target's last other active ActionBarSet effect is shown again, or the bar is cleared.
    /// TODO: which bar to use isn't known, dataBits02 may pick it. Every set goes on the <see cref="ShortcutSet.FloatingSpellBar"/>, the bar NexusForever shows the housing set on,
    /// which leaves the player's own abilities alone if it is the wrong one. The associated unit is the caster (inferred), the turret or vehicle for the bars cast by one.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.ActionBarSet)]
    public class SpellEffectActionBarSetHandler : SpellEffectCombatEffectHandler<ISpellEffectActionBarSetData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectActionBarSetData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            // 259 of the 405 rows are CancelOnly and 145 have a duration, the bar of the one with neither stays
            if (IsLasting(data))
                base.Apply(executionContext, player, info, data);

            SendActionBar(player, data.ShortcutSetId, executionContext.Spell.Caster.Guid);
            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectActionBarSetData data)
        {
            if (target is not IPlayer player || !IsLasting(data))
                return;

            base.Remove(spell, player, info, data);

            ISpellTargetEffectInfo previous = player.CombatEffectManager.GetEffects(SpellEffectType.ActionBarSet).LastOrDefault();
            ISpellEffectActionBarSetData previousData = player.CombatEffectManager.GetEffectData<ISpellEffectActionBarSetData>(SpellEffectType.ActionBarSet).LastOrDefault();
            if (previous != null && previousData != null)
                SendActionBar(player, previousData.ShortcutSetId, previous.Target.Collection.Spell.Caster.Guid);
            else
                SendActionBar(player, 0u, 0u);
        }

        private static bool IsLasting(ISpellEffectActionBarSetData data)
        {
            return data.Entry.DurationTime != 0u || data.Entry.Flags.HasFlag(SpellEffectFlags.CancelOnly);
        }

        private static void SendActionBar(IPlayer player, uint shortcutSetId, uint associatedUnitId)
        {
            player.Session.EnqueueMessageEncrypted(new ServerShowActionBar
            {
                ShortcutSet            = ShortcutSet.FloatingSpellBar,
                ActionBarShortcutSetId = (ushort)shortcutSetId,
                AssociatedUnitId       = associatedUnitId
            });
        }
    }
}
