using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ChangePlane)]
    public class SpellEffectChangePlaneHandler : ISpellEffectApplyHandler<ISpellEffectDefaultData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        /// <remarks>
        /// dataBits00 and dataBits01 are always equal: 4 on Void Slip ("Transitions the caster into the Void") and Dimensional Rift for their duration,
        /// 5 on Add Boss to Void and 1 on Remove Boss from Void. dataBits04 (1 on 11 Void Slip rows) isn't identified.
        /// </remarks>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDefaultData data)
        {
            // TODO: NexusForever has no planes and no message to tell the client about one is known
            return SpellEffectExecutionResult.Ok;
        }
    }
}
