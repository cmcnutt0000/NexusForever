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
    /// Stops the target queueing for a kind of content while the effect is active, the deserter debuffs.
    /// </summary>
    /// <remarks>
    /// TODO: NexusForever's matching has no eligibility, MatchingQueueValidator doesn't check one.
    /// The client keeps the flags of ServerMatchingEligibilityChanged (0x1405C41C0, matching manager +0xA0) but how dataBits00 maps to those flags isn't known, so they aren't sent either.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.SetMatchingEligibility)]
    public class SpellEffectSetMatchingEligibilityHandler : ISpellEffectApplyHandler<ISpellEffectSetMatchingEligibilityData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSetMatchingEligibilityData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
