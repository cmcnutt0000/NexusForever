using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.RestedXpDecorBonus)]
    public class SpellEffectRestedXpDecorBonusHandler : ISpellEffectApplyHandler<ISpellEffectRestedXpDecorBonusData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectRestedXpDecorBonusData data)
        {
            // TODO: rest experience is only worked out at login from where the player logged out (XpManager.CalculateRestXpAtLogin),
            // the decor bonus active at logout isn't stored
            return SpellEffectExecutionResult.Ok;
        }
    }
}
