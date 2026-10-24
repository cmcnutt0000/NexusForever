using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ProxyChannelVariableTime)]
    public class SpellEffectProxyChannelVariableTimeHandler : ISpellEffectApplyHandler<ISpellEffectProxyChannelVariableTimeData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectProxyChannelVariableTimeData data)
        {
            if (data.Spells.Count == 0)
                return SpellEffectExecutionResult.PreventEffect;

            executionContext.AddProxy(new ScheduledProxy(target, executionContext.Spell, executionContext.Spell.Parameters,
                data.Spells.Select(s => new ScheduledProxy.ProxySpell(s.Spell4Id, s.Delay))));
            return SpellEffectExecutionResult.Ok;
        }
    }
}
