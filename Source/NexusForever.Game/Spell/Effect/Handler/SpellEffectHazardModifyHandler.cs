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
    /// Changes the target's meter of a hazard, eg: "Water Bomb" "Reduces Dehydration by 30."
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120) and tests for no effect of it, the work is the server's.
    /// 25 of the 49 rows tick, the change is per tick then.
    /// TODO: NexusForever has no hazards (see <see cref="SpellEffectHazardEnableHandler"/>), what dataBits01, dataBits02 and dataBits06 change isn't known.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.HazardModify)]
    public class SpellEffectHazardModifyHandler : ISpellEffectApplyHandler<ISpellEffectHazardModifyData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectHazardModifyData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
