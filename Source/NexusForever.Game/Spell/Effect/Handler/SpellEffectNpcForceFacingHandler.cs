using System.Numerics;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.NpcForceFacing)]
    public class SpellEffectNpcForceFacingHandler : ISpellEffectApplyHandler<ISpellEffectNpcForceFacingData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectNpcForceFacingData data)
        {
            if (target is IPlayer)
                return SpellEffectExecutionResult.PreventEffect;

            // yaw is the X component of the rotation in radians
            Vector3 rotation = target.MovementManager.GetRotation();
            target.MovementManager.SetRotation(rotation with { X = rotation.X + data.Angle * MathF.PI / 180f }, true);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
