using NexusForever.Game.Abstract.Combat;
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
    /// Handler of an effect that puts state on the target's <see cref="ICombatEffectManager"/> while it is active.
    /// </summary>
    public abstract class SpellEffectCombatEffectHandler<T> : ISpellEffectApplyHandler<T>, ISpellEffectRemoveHandler<T> where T : ISpellEffectData
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public virtual SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, T data)
        {
            target.CombatEffectManager.AddEffect(info, data);
            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public virtual void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, T data)
        {
            target.CombatEffectManager.RemoveEffect(info);
        }
    }
}
