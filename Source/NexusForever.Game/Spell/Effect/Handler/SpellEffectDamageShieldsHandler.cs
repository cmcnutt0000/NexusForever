using NexusForever.Game.Abstract.Combat;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Proc;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Shared;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <remarks>
    /// The damage is only taken by shields, see <see cref="IDamageCalculator.CalculateDamage"/>.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.DamageShields)]
    public class SpellEffectDamageShieldsHandler : SpellEffectDamageHandler
    {
        public SpellEffectDamageShieldsHandler(
            IFactory<IDamageCalculator> damageCalculatorFactory,
            IFactory<IProcParameters> procParameterFactory)
            : base(damageCalculatorFactory, procParameterFactory)
        {
        }
    }
}
