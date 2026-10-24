using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Entity.Movement.Spline;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.NPCForceAIMovement)]
    public class SpellEffectNPCForceAIMovementHandler : ISpellEffectApplyHandler<ISpellEffectDefaultData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        /// <remarks>
        /// The only effect is Pet Command - Go To Location (41693), "Commands your bots to go to a location and stay there".
        /// It targets the caster and its implicit targets, the location is the spell's positional unit (Creature2 4064).
        /// </remarks>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDefaultData data)
        {
            IUnitEntity owner = executionContext.Spell.Caster;
            if (target.SummonerGuid != owner.Guid)
                return SpellEffectExecutionResult.Ok;

            // TODO: Spell.CanCast summons the positional unit at the caster rather than the targeted location
            uint positionalUnitId = executionContext.Spell.Parameters.PositionalUnitId;
            IWorldEntity location = positionalUnitId != 0u ? owner.GetVisible<IWorldEntity>(positionalUnitId) : null;
            if (location == null)
                return SpellEffectExecutionResult.PreventEffect;

            // TODO: NexusForever has no pet AI to keep the pet at the location afterwards
            // same speed as IMovementManager.Chase
            float speed = target.GetPropertyValue(Property.MoveSpeedMultiplier) * 8f;
            target.MovementManager.LaunchSpline([target.Position, location.Position], SplineType.Linear, SplineMode.OneShot, speed);

            return SpellEffectExecutionResult.Ok;
        }
    }
}
