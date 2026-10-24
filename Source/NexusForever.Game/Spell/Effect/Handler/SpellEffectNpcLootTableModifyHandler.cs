using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.NpcLootTableModify)]
    public class SpellEffectNpcLootTableModifyHandler : ISpellEffectApplyHandler<ISpellEffectLootTableData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectLootTableData data)
        {
            // TODO: NexusForever has no loot tables, the target creature's loot table would be replaced ("Mickey's change loot table on unit")
            return SpellEffectExecutionResult.Ok;
        }
    }
}
