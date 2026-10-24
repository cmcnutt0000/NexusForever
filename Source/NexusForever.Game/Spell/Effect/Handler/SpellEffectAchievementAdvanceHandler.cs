using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.AchievementAdvance)]
    public class SpellEffectAchievementAdvanceHandler : ISpellEffectApplyHandler<ISpellEffectAchievementAdvanceData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectAchievementAdvanceData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            // the advanced achievements (types 12, 66 and 104) have no object id to match an update with
            player.AchievementManager.AdvanceAchievement(player, (ushort)data.AchievementId, data.Count);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
