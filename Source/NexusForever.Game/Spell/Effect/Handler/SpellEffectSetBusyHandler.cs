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
    /// Sets the target busy or no longer busy, mostly on the objects quest activations use, eg: "Clearing Weeds", "Free Hostage".
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120) and tests for no effect of it, the work is the server's.
    /// Spells that set busy for a while clear it with a delayed second effect, eg: "Instant Activate + Busy (10 sec)".
    /// TODO: NexusForever units have no busy state, it likely stops other players activating the unit (GenericError TargetBusy, inferred).
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.SetBusy)]
    public class SpellEffectSetBusyHandler : ISpellEffectApplyHandler<ISpellEffectSetBusyData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSetBusyData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
