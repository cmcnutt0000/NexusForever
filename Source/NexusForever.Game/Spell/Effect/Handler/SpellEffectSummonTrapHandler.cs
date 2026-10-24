using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Entity.Creature;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.SummonTrap)]
    public class SpellEffectSummonTrapHandler : ISpellEffectApplyHandler<ISpellEffectSummonTrapData>
    {
        #region Dependency Injection

        private readonly IEntityFactory entityFactory;
        private readonly ICreatureInfoManager creatureInfoManager;

        public SpellEffectSummonTrapHandler(
            IEntityFactory entityFactory,
            ICreatureInfoManager creatureInfoManager)
        {
            this.entityFactory       = entityFactory;
            this.creatureInfoManager = creatureInfoManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        /// <remarks>
        /// Mines and fields placed at the target: Proximity Mine, Tether Mine, Mirage, Rejuvenator, quest traps.
        /// Every effect has a duration, the trap removes itself when it ends.
        /// </remarks>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSummonTrapData data)
        {
            ICreatureInfo creatureInfo = creatureInfoManager.GetCreatureInfo(data.CreatureId);
            if (creatureInfo == null)
                return SpellEffectExecutionResult.PreventEffect;

            IUnitEntity owner = executionContext.Spell.Caster;

            // TODO: dataBits03 (1 on 19 rows), dataBits07 (1 on 10) and dataBits08 (1 on 1) aren't identified
            var trap = entityFactory.CreateEntity<ITrapEntity>();
            trap.Initialise(owner, creatureInfo, data.SpellId, data.Radius, data.PrerequisiteId, data.EnemiesOnly, data.ArmTime, info.GetDuration());
            trap.Rotation     = target.Rotation;
            trap.SummonerGuid = owner.Guid;
            trap.AddToMap(owner.Map, target.Position);

            return SpellEffectExecutionResult.Ok;
        }
    }
}
