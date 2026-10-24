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
    /// Shows another spell's icon on a spell of the target's action bar while the effect is active, eg: the Warrior Shred icons.
    /// </summary>
    /// <remarks>
    /// The client does it all with its own class for the effect, on the local player only: it changes the icon (0x1405D9A20), puts it back when the effect goes (0x1405D99D0)
    /// and stops the player's older ChangeIcon effects of a matching spell from putting theirs back. The server has no state to change.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.ChangeIcon)]
    public class SpellEffectChangeIconHandler : ISpellEffectApplyHandler<ISpellEffectChangeIconData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectChangeIconData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
