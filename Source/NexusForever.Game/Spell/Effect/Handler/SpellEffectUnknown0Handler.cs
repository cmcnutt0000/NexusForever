using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Entity;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <summary>
    /// Effect type 0, the 18 rows of "VIP Bonus - XP and Rep Boost" Tier 1 to 3 (80504 to 80506), applied as UnitPropertyModifier effects (inferred).
    /// </summary>
    /// <remarks>
    /// Type 0 has no SpellEffectType row: the client's effect check (0x140567AF0) rejects such a row (it returns 0x67) and its factory makes a plain effect (0x140718120).
    /// All 18 rows are laid out as UnitPropertyModifier is (property, priority 4, multiplier 1.0, flat value), and the tooltips read the flat value as the bonus:
    /// "Increases XP gained by $(eff=213252.data03pct) and Reputation gained by $(eff=213259.data03pct)." where 213252 is on XpMultiplier (131).
    /// That the server applied them as property modifiers is inferred from that, they are read with <see cref="ISpellEffectUnitPropertyModifierData"/>.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.Unknown0)]
    public class SpellEffectUnknown0Handler : ISpellEffectApplyHandler<ISpellEffectUnitPropertyModifierData>, ISpellEffectRemoveHandler<ISpellEffectUnitPropertyModifierData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectUnitPropertyModifierData data)
        {
            if (!Enum.IsDefined(data.Property))
                return SpellEffectExecutionResult.PreventEffect;

            var modifier = new SpellPropertyModifier(data.Property, data.Priority, data.PercentageModifier, data.FlatValueModifier, data.LevelScalingModifier);
            target.AddSpellModifierProperty(modifier, executionContext.Spell.Parameters.SpellInfo.Entry.Id);
            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectUnitPropertyModifierData data)
        {
            if (!Enum.IsDefined(data.Property))
                return;

            target.RemoveSpellProperty(data.Property, spell.Parameters.SpellInfo.Entry.Id);
        }
    }
}
