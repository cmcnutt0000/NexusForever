using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ClampVital)]
    public class SpellEffectClampVitalHandler : SpellEffectCombatEffectHandler<ISpellEffectClampVitalData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectClampVitalData data)
        {
            if (!target.IsAlive)
                return SpellEffectExecutionResult.PreventEffect;

            base.Apply(executionContext, target, info, data);

            // health is clamped as it changes, a change of nothing applies the new clamp straight away
            target.ModifyHealth(0u, DamageType.Physical, executionContext.Spell.Caster);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
