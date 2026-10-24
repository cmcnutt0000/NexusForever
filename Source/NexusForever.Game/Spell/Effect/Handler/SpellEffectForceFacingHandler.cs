using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ForceFacing)]
    public class SpellEffectForceFacingHandler : ISpellEffectApplyHandler<ISpellEffectDefaultData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDefaultData data)
        {
            // the target faces the caster, a caster targeting itself faces its own target
            IUnitEntity caster = executionContext.Spell.Caster;
            uint? faceUnitId = target == caster ? caster.TargetGuid : caster.Guid;
            if (faceUnitId == null || faceUnitId == target.Guid)
                return SpellEffectExecutionResult.PreventEffect;

            target.MovementManager.SetRotationFaceUnit(faceUnitId.Value);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
