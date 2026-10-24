using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Network.World.Combat;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.SpellDispel)]
    public class SpellEffectSpellDispelHandler : ISpellEffectApplyHandler<ISpellEffectSpellDispelData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSpellDispelData data)
        {
            uint removed = 0u;
            foreach (ISpellTargetInfo targetInfo in target.GetAffectingSpells().ToList())
            {
                if (removed >= data.Count)
                    break;

                ISpell spell = targetInfo.Collection.Spell;
                if (targetInfo.IsFinalised || spell == executionContext.Spell)
                    continue;

                if (spell.Parameters.SpellInfo.BaseInfo.Entry.SpellClass != data.SpellClass)
                    continue;

                targetInfo.Finish();
                removed++;

                executionContext.AddCombatLog(new CombatLogDispel
                {
                    BRemovesSingleInstance = true,
                    InstancesRemoved       = 1u,
                    SpellRemovedId         = spell.Spell4Id
                });
            }

            return SpellEffectExecutionResult.Ok;
        }
    }
}
