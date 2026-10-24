using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Combat;
using NexusForever.Game.Prerequisite;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.SapVital)]
    public class SpellEffectSapVitalHandler : ISpellEffectApplyHandler<ISpellEffectSapVitalData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSapVitalData data)
        {
            if (!target.IsAlive)
                return SpellEffectExecutionResult.PreventEffect;

            float amount = target.GetVitalMax(data.Vital) * data.Fraction;
            target.AddVital(data.Vital, data.Restore ? amount : -amount, executionContext.Spell.Caster);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
