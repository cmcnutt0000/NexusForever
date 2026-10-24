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
    /// Upgrades the warplot plug of the target, "Upgrades a Warplot modification."
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120) and tests for no effect of it, the work is the server's.
    /// TODO: NexusForever has no warplots or warplot plugs (HousingWarplotPlugInfo, ServerWarPlotPlugState).
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.WarplotPlugUpgrade)]
    public class SpellEffectWarplotPlugUpgradeHandler : ISpellEffectApplyHandler<ISpellEffectDefaultData>
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
