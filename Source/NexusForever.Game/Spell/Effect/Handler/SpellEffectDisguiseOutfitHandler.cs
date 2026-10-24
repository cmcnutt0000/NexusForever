using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <summary>
    /// Dresses the target in a creature outfit, a weapon display and one more item display while the effect is active, eg: the hazmat suits and space helmets.
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120), so it shows only what the server sends: the outfit and displays go into the target's visual updates, see <see cref="SpellEffectVisualOverlay"/>.
    /// The client applies the outfit of a visual update to any unit (0x1403DBB70), whether it shows over a player's armour isn't known.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.DisguiseOutfit)]
    public class SpellEffectDisguiseOutfitHandler : SpellEffectCombatEffectHandler<ISpellEffectDisguiseOutfitData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDisguiseOutfitData data)
        {
            base.Apply(executionContext, target, info, data);
            target.SetVisualEmit(true);
            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDisguiseOutfitData data)
        {
            base.Remove(spell, target, info, data);
            target.SetVisualEmit(true);
        }
    }
}
