using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ModifyCreatureFlags)]
    public class SpellEffectModifyCreatureFlagsHandler : ISpellEffectApplyHandler<ISpellEffectDefaultData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        /// <remarks>
        /// Two flag changes: dataBits00 sets (1) or clears (0) the flags in dataBits01, dataBits02 does the same for dataBits03.
        /// [Generic] Set Flag SpellCannotBeSelection is (1, 0x1) and Remove Flag SpellCannotBeSelection (0, 0x1),
        /// the burrowing attacks (Rumble, Burrowing Assault) set 0x4020 in the second set for their duration.
        /// </remarks>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDefaultData data)
        {
            // TODO: NexusForever has no unit flags at run time for these to change and which fields the two sets are isn't known
            return SpellEffectExecutionResult.Ok;
        }
    }
}
