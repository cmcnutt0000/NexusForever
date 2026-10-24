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
    /// Applies the player's pending Limited Action Set changes, the one row is spell 63684 "Class - Update Action Set - Gates applying new LAS Bars".
    /// </summary>
    /// <remarks>
    /// The client doesn't reference the spell, so the server casts it and the spell's cast conditions gate the change (inferred from its description).
    /// TODO: NexusForever applies the changes as ClientRequestActionSetChanges arrives instead of keeping them until this effect, so there is nothing left to apply.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.ApplyLASChanges)]
    public class SpellEffectApplyLASChangesHandler : ISpellEffectApplyHandler<ISpellEffectDefaultData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDefaultData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
