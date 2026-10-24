using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Network.World.Combat;
using NexusForever.Network.World.Message.Static;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <remarks>
    /// TODO: DataBits01 is a lockout in milliseconds and DataBits03 the abilities it locks, eg: "Weapon Lock: You are unable to cast melee abilities.", there are no lockouts yet.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.SpellCounter)]
    public class SpellEffectSpellCounterHandler : ISpellEffectApplyHandler<ISpellEffectDefaultData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDefaultData data)
        {
            if (!target.HasSpell(s => s.IsCasting && s != executionContext.Spell, out ISpell spell))
                return SpellEffectExecutionResult.Ok;

            spell.CancelCast(CastResult.SpellCountered);

            executionContext.AddCombatLog(new CombatLogInterrupted
            {
                InterruptingSpellId = executionContext.Spell.Spell4Id,
                Reason              = (ushort)CastResult.SpellCountered,
                CastData            = new CombatLogCastData
                {
                    CasterId     = executionContext.Spell.Caster.Guid,
                    TargetId     = target.Guid,
                    SpellId      = spell.Spell4Id,
                    CombatResult = CombatResult.Hit
                }
            });

            return SpellEffectExecutionResult.Ok;
        }
    }
}
