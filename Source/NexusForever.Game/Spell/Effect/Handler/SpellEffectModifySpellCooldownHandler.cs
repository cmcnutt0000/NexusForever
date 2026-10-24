using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ModifySpellCooldown)]
    public class SpellEffectModifySpellCooldownHandler : SpellEffectCombatEffectHandler<ISpellEffectModifySpellCooldownData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectModifySpellCooldownData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.PreventEffect;

            // the cooldowns already running change straight away
            player.SpellManager.UpdateSpellCooldowns(data.Matches, data.Apply);

            // an effect that lasts also changes the cooldowns of its SpellCoolDown id started while it is active, see 0x14046B7F0 and 0x14046A760 in the client
            if (info.Entry.DurationTime > 0u || info.Entry.Flags.HasFlag(SpellEffectFlags.CancelOnly))
                base.Apply(executionContext, target, info, data);

            return SpellEffectExecutionResult.Ok;
        }
    }
}
