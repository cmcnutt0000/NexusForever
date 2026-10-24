using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.PVP;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Network.World.Message.Model.Pvp;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <summary>
    /// The player isn't flagged for PvP while the effect is active, spell 52589 "PvP - World PvP - Temporarily Not PvP Flagged" (30s).
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120) and tests for no effect of it, it learns a unit's PvP state from <see cref="ServerUnitPvpStateChange"/>.
    /// The player's own <see cref="IPlayer.PvPFlags"/> stay as they are: the visible state is sent without <see cref="PvPFlag.Enabled"/> while the effect is active and
    /// <see cref="IUnitEntity.CanAttack"/> of other players treats the player as not flagged, the real flags are sent again when the last such effect goes.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.TemporarilyUnflagPvp)]
    public class SpellEffectTemporarilyUnflagPvpHandler : SpellEffectCombatEffectHandler<ISpellEffectDefaultData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDefaultData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            base.Apply(executionContext, player, info, data);
            SendPvpState(player, player.PvPFlags & ~PvPFlag.Enabled);
            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDefaultData data)
        {
            if (target is not IPlayer player)
                return;

            base.Remove(spell, player, info, data);
            if (!player.CombatEffectManager.HasEffect(SpellEffectType.TemporarilyUnflagPvp))
                SendPvpState(player, player.PvPFlags);
        }

        private static void SendPvpState(IPlayer player, PvPFlag state)
        {
            player.EnqueueToVisible(new ServerUnitPvpStateChange
            {
                UnitId = player.Guid,
                State  = state
            }, true);
        }
    }
}
