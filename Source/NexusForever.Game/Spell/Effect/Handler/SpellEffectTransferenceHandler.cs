using NexusForever.Game.Abstract.Combat;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Combat;
using NexusForever.Game.Prerequisite;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Shared;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.Transference)]
    public class SpellEffectTransferenceHandler : ISpellEffectApplyHandler<ISpellEffectTransferenceData>
    {
        #region Dependency Injection

        private readonly IFactory<IDamageCalculator> damageCalculatorFactory;

        public SpellEffectTransferenceHandler(
            IFactory<IDamageCalculator> damageCalculatorFactory)
        {
            this.damageCalculatorFactory = damageCalculatorFactory;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectTransferenceData data)
        {
            IUnitEntity caster = executionContext.Spell.Caster;
            if (!target.CanAttack(caster))
                return SpellEffectExecutionResult.PreventEffect;

            IDamageCalculator damageCalculator = damageCalculatorFactory.Resolve();

            float drained;
            if (data.DrainVital == Vital.Health)
            {
                damageCalculator.CalculateDamage(executionContext, target, info);
                if (info.Damage == null)
                    return SpellEffectExecutionResult.Ok;

                target.TakeDamage(caster, info.Damage);
                drained = info.Damage.AdjustedDamage;
            }
            else
            {
                drained = MathF.Min(damageCalculator.CalculateEffectMagnitude(caster, target, info.Entry), target.GetVital(data.DrainVital));
                target.AddVital(data.DrainVital, -drained, caster);
            }

            if (drained > 0f && data.GainFraction > 0f)
                caster.AddVital(data.GainVital, drained * data.GainFraction, caster);

            return SpellEffectExecutionResult.Ok;
        }
    }
}
