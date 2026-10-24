using System.Runtime.CompilerServices;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ProxyChannel)]
    public class SpellEffectProxyChannelHandler : ISpellEffectApplyHandler<ISpellEffectProxyChannelData>
    {
        // executions of each effect, a channel executes its effects after its initial delay and on each pulse
        private static readonly ConditionalWeakTable<ISpellTargetEffectInfo, StrongBox<int>> executions = new();

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectProxyChannelData data)
        {
            int execution = executions.GetOrCreateValue(info).Value++;
            if (execution >= data.Spells.Count || data.Spells[execution] == 0u)
                return SpellEffectExecutionResult.Ok;

            executionContext.AddProxy(new ScheduledProxy(target, executionContext.Spell, executionContext.Spell.Parameters, [new ScheduledProxy.ProxySpell(data.Spells[execution], 0d)]));
            return SpellEffectExecutionResult.Ok;
        }
    }
}
