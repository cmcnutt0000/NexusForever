using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.VendorPriceModifier)]
    public class SpellEffectVendorPriceModifierHandler : ISpellEffectApplyHandler<ISpellEffectVendorPriceModifierData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectVendorPriceModifierData data)
        {
            // TODO: the vendor handlers only apply the vendor's own multipliers (IVendorInfo), these would multiply them while the effect is active
            return SpellEffectExecutionResult.Ok;
        }
    }
}
