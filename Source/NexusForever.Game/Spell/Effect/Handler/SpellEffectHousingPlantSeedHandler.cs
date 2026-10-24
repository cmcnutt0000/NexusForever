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
    /// Sows a plant in a housing garden plot, the 20 "Plant Seeds - <plant> (signal)" spells.
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120) and tests for no effect of it, the work is the server's.
    /// TODO: NexusForever's housing has no garden plots or plants to sow, and what the seed id maps to (a plant, its growth and its harvest) isn't in the tables.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.HousingPlantSeed)]
    public class SpellEffectHousingPlantSeedHandler : ISpellEffectApplyHandler<ISpellEffectHousingPlantSeedData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectHousingPlantSeedData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
