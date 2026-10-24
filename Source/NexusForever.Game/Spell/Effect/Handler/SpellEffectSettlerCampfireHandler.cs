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
    /// Builds the Settler's campfire, "Builds a campfire for people to warm themselves by.", for an hour.
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120) and tests for no effect of it, the work is the server's.
    /// The same spells proxy spell 32760 (and 32775, 32776) to their target, a heal every second, a fluff and a DespawnUnit an hour later, so the effect likely makes the campfire unit (inferred).
    /// TODO: which unit the campfire is isn't known, dataBits01 (91) isn't a Creature2 id.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.SettlerCampfire)]
    public class SpellEffectSettlerCampfireHandler : ISpellEffectApplyHandler<ISpellEffectSettlerCampfireData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSettlerCampfireData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
