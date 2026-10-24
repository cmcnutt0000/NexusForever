using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ModifyAbilityCharges)]
    public class SpellEffectModifyAbilityChargesHandler : SpellEffectCombatEffectHandler<ISpellEffectModifyAbilityChargesData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectModifyAbilityChargesData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.PreventEffect;

            // the extra maximum charges are read from the effect while it is active, see ICharacterSpell.MaxAbilityCharges
            if (data.MaximumCharges)
                base.Apply(executionContext, target, info, data);

            player.SpellManager.GetSpell(data.Spell4BaseId)?.AddAbilityCharges(data.Charges);
            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectModifyAbilityChargesData data)
        {
            base.Remove(spell, target, info, data);

            // the charges over the lower maximum are lost
            if (data.MaximumCharges && target is IPlayer player)
                player.SpellManager.GetSpell(data.Spell4BaseId)?.AddAbilityCharges(0);
        }
    }
}
