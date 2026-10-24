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
    /// Kills the player so they can come back at a resurrection point, the /stuck spells "Support Stuck - Free Suicide" and "Support Stuck - Durability Loss Suicide".
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120), the death is the server's and is dealt as <see cref="SpellEffectKillHandler"/> does.
    /// TODO: NexusForever has no durability loss on death, so <see cref="ISpellEffectSupportStuckData.IsFree"/> changes nothing.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.SupportStuck)]
    public class SpellEffectSupportStuckHandler : ISpellEffectApplyHandler<ISpellEffectSupportStuckData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSupportStuckData data)
        {
            if (target is not IPlayer || !target.IsAlive)
                return SpellEffectExecutionResult.PreventEffect;

            target.ModifyHealth(target.Health, DamageType.Physical, executionContext.Spell.Caster);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
