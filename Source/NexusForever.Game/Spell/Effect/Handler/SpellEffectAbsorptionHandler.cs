using NexusForever.Game.Abstract.Combat;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Shared;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.Absorption)]
    public class SpellEffectAbsorptionHandler : SpellEffectCombatEffectHandler<ISpellEffectAbsorptionData>
    {
        #region Dependency Injection

        private readonly IFactory<IDamageCalculator> damageCalculatorFactory;

        public SpellEffectAbsorptionHandler(
            IFactory<IDamageCalculator> damageCalculatorFactory)
        {
            this.damageCalculatorFactory = damageCalculatorFactory;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectAbsorptionData data)
        {
            bool hasParameters = data.Entry.ParameterType.Any(p => p != SpellEffectParameterType.None);
            if (data.Amount == 0f && !hasParameters)
            {
                // 141 effects have no amount or parameters, eg: the creature buff "Absorb: Absorbs all damage types", they absorb everything for their duration
                target.CombatEffectManager.AddAbsorption(info, data, uint.MaxValue);
                return SpellEffectExecutionResult.Ok;
            }

            // (DataBits01 + magnitude) * DataBits00, eg: "absorbing damage equal to 1000% of your Wisdom" only has parameters
            float amount = data.Amount;
            if (hasParameters)
                amount += damageCalculatorFactory.Resolve().CalculateEffectMagnitude(executionContext.Spell.Caster, target, data.Entry);
            amount *= data.Multiplier != 0f ? data.Multiplier : 1f;

            target.CombatEffectManager.AddAbsorption(info, data, (uint)MathF.Round(MathF.Max(0f, amount)));
            return SpellEffectExecutionResult.Ok;
        }
    }
}
