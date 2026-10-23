using NexusForever.Game.Abstract.Combat;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Entity;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Shared;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.UnitPropertyModifier)]
    public class SpellEffectUnitPropertyModifierHandler : ISpellEffectApplyHandler<ISpellEffectUnitPropertyModifierData>, ISpellEffectRemoveHandler<ISpellEffectUnitPropertyModifierData>
    {
        #region Dependency Injection

        private readonly IFactory<IDamageCalculator> damageCalculatorFactory;

        public SpellEffectUnitPropertyModifierHandler(
            IFactory<IDamageCalculator> damageCalculatorFactory)
        {
            this.damageCalculatorFactory = damageCalculatorFactory;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectUnitPropertyModifierData data)
        {
            // 284 property modifiers have parameters (219 of them the item budget), their magnitude adds to the flat value
            // the client shows it in tooltips, see 0x1404DE840; 258 of them have no flat value of their own
            float flatValue = data.FlatValueModifier;
            if (data.Entry.ParameterType.Any(p => p != SpellEffectParameterType.None))
                flatValue += damageCalculatorFactory.Resolve().CalculateEffectMagnitude(executionContext.Spell.Caster, target, data.Entry);

            // TODO: I suppose these could be cached somewhere instead of generating them every single effect?
            var modifier = new SpellPropertyModifier(data.Property, data.Priority, data.PercentageModifier, flatValue, data.LevelScalingModifier);
            target.AddSpellModifierProperty(modifier, executionContext.Spell.Parameters.SpellInfo.Entry.Id);

            // TODO: not sure if there is a better place for this...
            if (data.Property == Property.InterruptArmorThreshold)
            {
                if (target.MaxInterruptArmour == -1)
                    target.InterruptArmour = 0;
                else
                    target.InterruptArmour = target.MaxInterruptArmour;
            }

            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectUnitPropertyModifierData data)
        {
            target.RemoveSpellProperty(data.Property, spell.Parameters.SpellInfo.Entry.Id);
        }
    }
}
