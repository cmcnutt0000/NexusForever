using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Entity.Creature;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.SummonVehicle)]
    public class SpellEffectSummonVehicleHandler : ISpellEffectApplyHandler<ISpellEffectSummonVehicleData>, ISpellEffectRemoveHandler<ISpellEffectSummonVehicleData>
    {
        #region Dependency Injection

        private readonly IEntityFactory entityFactory;
        private readonly ICreatureInfoManager creatureInfoManager;

        public SpellEffectSummonVehicleHandler(
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
        /// Quest speeders, housing airships and raid war mechs, the target is put in the pilot seat.
        /// </remarks>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSummonVehicleData data)
        {
            // TODO: only players can be passengers in NexusForever, Chua Summon Mount targets another unit
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.PreventEffect;

            if (!player.CanMount())
                return SpellEffectExecutionResult.PreventEffect;

            ICreatureInfo creatureInfo = creatureInfoManager.GetCreatureInfo(data.CreatureId);
            if (creatureInfo == null)
                return SpellEffectExecutionResult.PreventEffect;

            // each of the UnitVehicle entries has a single pilot seat
            var vehicle = entityFactory.CreateEntity<IVehicleEntity>();
            vehicle.Initialise(creatureInfo, data.VehicleId, executionContext.Spell.Parameters.SpellInfo.Entry.Id);
            vehicle.Rotation = player.Rotation;
            vehicle.EnqueuePassengerAdd(player, VehicleSeatType.Pilot, 0);
            vehicle.AddToMap(player.Map, player.Position);

            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSummonVehicleData data)
        {
            // 10 effects last until the spell is cancelled, the vehicles of the other 2 stay until the pilot gets out
            if (data.Entry.DurationTime == 0u && !data.Entry.Flags.HasFlag(SpellEffectFlags.CancelOnly))
                return;

            if (target is not IPlayer player || player.PlatformGuid == null)
                return;

            if (player.GetVisible<IWorldEntity>(player.PlatformGuid.Value) is IVehicleEntity vehicle
                && vehicle.SpellEntry?.Id == spell.Spell4Id)
                player.Dismount();
        }
    }
}
