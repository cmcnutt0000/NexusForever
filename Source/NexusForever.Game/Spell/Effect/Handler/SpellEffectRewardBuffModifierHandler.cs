using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.RewardBuffModifier)]
    public class SpellEffectRewardBuffModifierHandler : ISpellEffectApplyHandler<ISpellEffectRewardBuffModifierData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectRewardBuffModifierData data)
        {
            // TODO: only three test spells use this, both values are 1.5 and there is no daily reward system to apply them to
            return SpellEffectExecutionResult.Ok;
        }
    }
}
