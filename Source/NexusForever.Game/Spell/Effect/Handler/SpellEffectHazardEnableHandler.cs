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
    /// Starts a hazard meter for the target while the effect is active, eg: "Poison Gas", "Ritual Timer".
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120) and tests for no effect of it, the work is the server's.
    /// TODO: NexusForever has no hazards: Hazard rows are loaded and ServerHazardList, ServerHazardAction and ServerHazardModifiers are modelled, but no meter is kept or sent.
    /// PrerequisiteCheckHazardActive and PrerequisiteCheckHazardValue would read the same state.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.HazardEnable)]
    public class SpellEffectHazardEnableHandler : ISpellEffectApplyHandler<ISpellEffectHazardEnableData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectHazardEnableData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
