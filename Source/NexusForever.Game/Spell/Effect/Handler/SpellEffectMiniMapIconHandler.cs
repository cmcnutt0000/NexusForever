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
    /// Shows the target on the map of other players while the effect is active, "Track Player": "Displays your presence on the map to all players for 15s."
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120) and tests for no effect of it, so the server has to send the unit's position.
    /// TODO: NexusForever sends no map tracking of units. ServerMapTrackedUnitUpdate and ServerMapTrackedUnitDisable are modelled (with a public event objective id) but never sent,
    /// whether they show this and what dataBits00 picks aren't known.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.MiniMapIcon)]
    public class SpellEffectMiniMapIconHandler : ISpellEffectApplyHandler<ISpellEffectMiniMapIconData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectMiniMapIconData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
