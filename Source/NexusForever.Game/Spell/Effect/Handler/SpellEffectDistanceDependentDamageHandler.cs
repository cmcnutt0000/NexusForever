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
    /// TODO: the damage changes with distance, the fall off fields (DataBits02 to DataBits05) aren't known yet.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.DistanceDependentDamage)]
    public class SpellEffectDistanceDependentDamageHandler : SpellEffectDamageHandler
    {
        public SpellEffectDistanceDependentDamageHandler(
            IFactory<IDamageCalculator> damageCalculatorFactory,
            IFactory<IProcParameters> procParameterFactory)
            : base(damageCalculatorFactory, procParameterFactory)
        {
        }
    }
}
