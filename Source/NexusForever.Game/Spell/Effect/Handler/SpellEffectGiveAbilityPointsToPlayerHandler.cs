using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.GiveAbilityPointsToPlayer)]
    public class SpellEffectGiveAbilityPointsToPlayerHandler : ISpellEffectApplyHandler<ISpellEffectGiveAbilityPointsToPlayerData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectGiveAbilityPointsToPlayerData data)
        {
            // TODO: bonus ability tier points aren't tracked, every action set starts with ActionSet.MaxTierPoints;
            // the spell's tooltip data allows 7 bonus and 48 total points (Spell4Base lasTierPointTooltipData00-01)
            return SpellEffectExecutionResult.Ok;
        }
    }
}
