using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <remarks>
    /// The client removes every cooldown on the unit, see 0x14046ACC0 in the client, eg: "Mordechai's treasure has renewed your spirits, refreshing cooldowns!".
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.CooldownReset)]
    public class SpellEffectCooldownResetHandler : ISpellEffectApplyHandler<ISpellEffectDefaultData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDefaultData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.PreventEffect;

            player.SpellManager.ResetAllSpellCooldowns();
            return SpellEffectExecutionResult.Ok;
        }
    }
}
