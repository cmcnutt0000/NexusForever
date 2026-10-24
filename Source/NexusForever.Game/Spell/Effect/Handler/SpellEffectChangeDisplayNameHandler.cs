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
    /// Shows another name on the target while the effect is active, eg: "Altered Rockhound Slasher".
    /// </summary>
    /// <remarks>
    /// The client does it all with its own class for the effect: it saves the target's name, sets the localised text in dataBits00 (0x1405D9DB0)
    /// and puts the saved name back when the effect is removed (0x1405D9D40) or destroyed (0x1405D9CB0). The server keeps no display name of its own to change.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.ChangeDisplayName)]
    public class SpellEffectChangeDisplayNameHandler : ISpellEffectApplyHandler<ISpellEffectChangeDisplayNameData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectChangeDisplayNameData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
