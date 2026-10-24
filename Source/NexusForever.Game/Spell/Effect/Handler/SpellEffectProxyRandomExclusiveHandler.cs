using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ProxyRandomExclusive)]
    public class SpellEffectProxyRandomExclusiveHandler : ISpellEffectApplyHandler<ISpellEffectProxyRandomExclusiveData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectProxyRandomExclusiveData data)
        {
            if (data.Spells.Count == 0)
                return SpellEffectExecutionResult.PreventEffect;

            // one spell is picked by weight, without weights each is as likely
            long total = data.Spells.Sum(s => (long)s.Weight);
            uint spell4Id = data.Spells[Random.Shared.Next(data.Spells.Count)].Spell4Id;
            if (total > 0)
            {
                long roll = Random.Shared.NextInt64(total);
                foreach ((uint id, uint weight) in data.Spells)
                {
                    if (roll < weight)
                    {
                        spell4Id = id;
                        break;
                    }

                    roll -= weight;
                }
            }

            executionContext.AddProxy(new ScheduledProxy(target, executionContext.Spell, executionContext.Spell.Parameters, [new ScheduledProxy.ProxySpell(spell4Id, 0d)]));
            return SpellEffectExecutionResult.Ok;
        }
    }
}
