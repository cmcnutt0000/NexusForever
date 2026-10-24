using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Network.World.Message.Static;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.GrantXP)]
    public class SpellEffectGrantXpHandler : ISpellEffectApplyHandler<ISpellEffectGrantXpData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectGrantXpData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            if (player.Level < data.MinLevel || player.Level > data.MaxLevel)
                return SpellEffectExecutionResult.Ok;

            player.XpManager.GrantXp(data.Xp, ExpReason.Spell);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
