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
    /// TODO: the damage changes with distance, DataBits02 (2, 3 and 4) is a DistanceDamageModifier id, a curve of 5 distance fractions and damage multipliers.
    /// The client caches the table (0x140560D90) but no damage code reads it, the damage effect class of 3, 8, 12, 33 and 138 is shared, so the curve was applied by the server only
    /// and what its distance is a fraction of isn't known.
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
