using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.GrantLevelScaledPrestige)]
    public class SpellEffectGrantLevelScaledPrestigeHandler : ISpellEffectApplyHandler<ISpellEffectGrantLevelScaledData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectGrantLevelScaledData data)
        {
            // TODO: the multiplier (0.4 to 11.25 on battleground, arena and warplot spells) scales an amount of prestige per level
            // that isn't in the client's tables, the result would go to CurrencyType.Prestige
            return SpellEffectExecutionResult.Ok;
        }
    }
}
