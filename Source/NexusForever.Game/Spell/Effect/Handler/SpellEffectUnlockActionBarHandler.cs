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
    /// Unlocks one of the player's action sets ("Unlock an additional Action set.").
    /// </summary>
    /// <remarks>
    /// TODO: NexusForever doesn't store which action sets are unlocked, every set is sent unlocked (<see cref="ActionSet.BuildServerActionSet"/>),
    /// so there is nothing to change. PrerequisiteCheckActionBar (type 239) would read the same state.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.UnlockActionBar)]
    public class SpellEffectUnlockActionBarHandler : ISpellEffectApplyHandler<ISpellEffectUnlockActionBarData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectUnlockActionBarData data)
        {
            if (data.ActionSetIndex >= ActionSet.MaxActionSets)
                return SpellEffectExecutionResult.PreventEffect;

            return SpellEffectExecutionResult.Ok;
        }
    }
}
