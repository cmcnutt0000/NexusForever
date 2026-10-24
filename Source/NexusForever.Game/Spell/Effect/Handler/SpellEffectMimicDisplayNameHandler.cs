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
    /// The caster shows the target's name while the effect is active, eg: the Stalker clone and the player holograms.
    /// </summary>
    /// <remarks>
    /// The client does it all with its own class for the effect: it saves the caster's name, gives it the target's (0x1405D9F80) and puts it back when the effect goes (0x1405D9F00).
    /// It reads no data, dataBits01 is 1 in 1 of the 4 rows. The server has no state to change.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.MimicDisplayName)]
    public class SpellEffectMimicDisplayNameHandler : ISpellEffectApplyHandler<ISpellEffectDefaultData>
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
