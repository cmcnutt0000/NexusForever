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
    /// The caster takes on the appearance of the target while the effect is active, eg: the Stalker clone and the player holograms.
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120), so the server sends the copied display, outfit, item visuals and, of a player, race and sex in the caster's visual updates, see <see cref="SpellEffectVisualOverlay"/>.
    /// That the caster copies the target is inferred from MimicDisplayName, the client's class for it (0x1405D9F80) gives the effect's caster (+0x58) the name of its target.
    /// TODO: bones and customisations of a player beyond its item visuals aren't copied, and a player caster keeps its own race and sex (<see cref="IPlayer"/> sets them after the overlay).
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.MimicDisguise)]
    public class SpellEffectMimicDisguiseHandler : ISpellEffectApplyHandler<ISpellEffectMimicDisguiseData>, ISpellEffectRemoveHandler<ISpellEffectMimicDisguiseData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectMimicDisguiseData data)
        {
            IUnitEntity caster = executionContext.Spell.Caster;
            if (caster == target)
                return SpellEffectExecutionResult.Ok;

            // kept by the caster, it is the one whose appearance changes
            caster.CombatEffectManager.AddEffect(info, data);
            caster.SetVisualEmit(true);
            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectMimicDisguiseData data)
        {
            IUnitEntity caster = spell.Caster;
            if (caster == target)
                return;

            caster.CombatEffectManager.RemoveEffect(info);
            caster.SetVisualEmit(true);
        }
    }
}
