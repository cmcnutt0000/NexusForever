using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.GiveAugmentPowerToPlayer)]
    public class SpellEffectGiveAugmentPowerToPlayerHandler : ISpellEffectApplyHandler<ISpellEffectGiveAugmentPowerToPlayerData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectGiveAugmentPowerToPlayerData data)
        {
            // TODO: bonus AMP power isn't tracked, ActionSet leaves its 10 bonus points locked; the bonus would be sent with ServerAmpPowerUpdate
            // and the spell's tooltip data allows 10 bonus and 57 total (Spell4Base lasTierPointTooltipData00-01)
            return SpellEffectExecutionResult.Ok;
        }
    }
}
