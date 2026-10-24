using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Entity;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <summary>
    /// Adds a fraction of one of the target's properties to another while the effect is active, eg: "Increase your Armor by 1% of your Max Health."
    /// </summary>
    /// <remarks>
    /// The client doesn't compute unit properties from auras (the server sends them), so the rule comes from the tooltips.
    /// The converted value is added as a flat spell modifier of priority 1, as most flat UnitPropertyModifier effects are.
    /// TODO: the value is taken when the effect applies, NexusForever has no way to recompute it when the source property changes, and "per stack" (Murderous Intent) isn't applied as there are no stacks.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.UnitPropertyConversion)]
    public class SpellEffectUnitPropertyConversionHandler : ISpellEffectApplyHandler<ISpellEffectUnitPropertyConversionData>, ISpellEffectRemoveHandler<ISpellEffectUnitPropertyConversionData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectUnitPropertyConversionData data)
        {
            if (!Enum.IsDefined(data.SourceProperty) || !Enum.IsDefined(data.Property))
                return SpellEffectExecutionResult.PreventEffect;

            float value = target.GetPropertyValue(data.SourceProperty) * data.Fraction;
            var modifier = new SpellPropertyModifier(data.Property, 1u, 1f, value, 0f);
            target.AddSpellModifierProperty(modifier, executionContext.Spell.Parameters.SpellInfo.Entry.Id);
            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectUnitPropertyConversionData data)
        {
            if (!Enum.IsDefined(data.Property))
                return;

            target.RemoveSpellProperty(data.Property, spell.Parameters.SpellInfo.Entry.Id);
        }
    }
}
