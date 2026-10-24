using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.GrantLevelScaledXP)]
    public class SpellEffectGrantLevelScaledXpHandler : ISpellEffectApplyHandler<ISpellEffectGrantLevelScaledData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectGrantLevelScaledData data)
        {
            // TODO: the multiplier (5 to 22.5 on battleground, warplot and holiday instance spells) scales an amount of experience
            // per level that isn't in the client's tables; XpPerLevel.BaseQuestXpPerLevel, which quest experience multiplies, is a candidate
            return SpellEffectExecutionResult.Ok;
        }
    }
}
