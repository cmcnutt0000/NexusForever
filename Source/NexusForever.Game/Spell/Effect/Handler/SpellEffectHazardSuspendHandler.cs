using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <summary>
    /// Stops a hazard's meter for the target while the effect is active, eg: "Hazmat Protection: Prevents poisoning for 30 minutes."
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120) and tests for no effect of it, the work is the server's.
    /// ServerHazardModifiers has a suspended flag per hazard, likely what this sets (inferred).
    /// TODO: NexusForever has no hazards (see <see cref="SpellEffectHazardEnableHandler"/>).
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.HazardSuspend)]
    public class SpellEffectHazardSuspendHandler : ISpellEffectApplyHandler<ISpellEffectHazardSuspendData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectHazardSuspendData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
