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
    /// Gives a facility to or takes one from the target, facilities mark units for creature AI and scripts, eg: "Requires creature 18728 with facility 6776".
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120) and tests for no effect of it, the work is the server's.
    /// PrerequisiteCheckFacilityWithinDistance (type 30) is the server's test for one, its client implementation always fails.
    /// TODO: NexusForever has no facilities on units, and only 3 of the 43 object ids of prerequisite 30 are facility ids here, so what that prerequisite looks for isn't settled.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.FacilityModification)]
    public class SpellEffectFacilityModificationHandler : ISpellEffectApplyHandler<ISpellEffectFacilityModificationData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectFacilityModificationData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
