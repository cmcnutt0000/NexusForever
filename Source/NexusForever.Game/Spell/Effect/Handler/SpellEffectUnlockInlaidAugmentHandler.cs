using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.UnlockInlaidAugment)]
    public class SpellEffectUnlockInlaidAugmentHandler : ISpellEffectApplyHandler<ISpellEffectUnlockInlaidAugmentData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectUnlockInlaidAugmentData data)
        {
            // TODO: unlocked AMPs aren't tracked, see PrerequisiteCheckAmpUnlocked
            return SpellEffectExecutionResult.Ok;
        }
    }
}
