using System.Numerics;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Entity.Creature;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Entity.Creature;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Shared;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.SummonCreature)]
    public class SpellEffectSummonCreatureHandler : ISpellEffectApplyHandler<ISpellEffectSummonCreatureData>, ISpellEffectRemoveHandler<ISpellEffectSummonCreatureData>
    {
        #region Dependency Injection

        private readonly ICreatureInfoManager creatureInfoManager;

        public SpellEffectSummonCreatureHandler(
            ICreatureInfoManager creatureInfoManager)
        {
            this.creatureInfoManager = creatureInfoManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        /// <remarks>
        /// The caster is the summoner, the summons are placed around the target.
        /// </remarks>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSummonCreatureData data)
        {
            ICreatureInfo creatureInfo = creatureInfoManager.GetCreatureInfo(data.CreatureId);
            if (creatureInfo == null)
                return SpellEffectExecutionResult.PreventEffect;

            // TODO: a vehicle needs its UnitVehicle from IVehicleEntity.Initialise, 6 rows summon player controlled turrets
            if (creatureInfo.Entry.CreationTypeEnum == EntityType.Vehicle)
                return SpellEffectExecutionResult.PreventEffect;

            IUnitEntity summoner = executionContext.Spell.Caster;
            ICreatureInfo summonInfo = GetSummonCreatureInfo(creatureInfo, summoner);

            for (uint i = 0u; i < data.Count; i++)
            {
                IWorldEntity summon = null;
                summon = summoner.SummonFactory.Summon(summonInfo, GetPosition(summoner, target, data), target.Rotation, (map, guid, vector) =>
                {
                    // the summon casts it on itself: Turret Duration on the Build Turret turrets, the Guardian Angel aura
                    if (data.SpellId != 0u && summon is IUnitEntity unit)
                        unit.CastSpell(data.SpellId, new SpellParameters());
                });
            }

            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSummonCreatureData data)
        {
            // 113 effects have no duration, their summons stay until they die or the summoner leaves the map
            if (info.GetDuration() == TimeSpan.Zero)
                return;

            // TODO: summons are matched by creature id, the end of one cast also removes the summons of a later one
            spell.Caster.SummonFactory.UnsummonCreature(data.CreatureId);
        }

        /// <summary>
        /// Return the summon position for <see cref="ISpellEffectSummonCreatureData"/> around the source <see cref="IUnitEntity"/>.
        /// </summary>
        private static Vector3 GetPosition(IUnitEntity summoner, IUnitEntity source, ISpellEffectSummonCreatureData data)
        {
            Vector3 position = source.Position;

            // each mine of Sling Spines and Pollen Mines has its own telegraph at a different offset
            if (data.TelegraphDamage != null)
                position = new Telegraph(data.TelegraphDamage, summoner, source.Position, source.Rotation).Position;
            else if (data.MaxDistance > 0u)
            {
                // the angle is in degrees from the source's facing (Moodie Mask Carrier places 4 summons at 0, 90, 180 and 270),
                // 360 is used where several summons are scattered around the source (Mine Bomb Release, Summon Swarm)
                float angle;
                if (data.Angle >= 360)
                    angle = (float)Random.Shared.NextDouble() * (MathF.PI * 2f);
                else
                    angle = -source.Rotation.X - MathF.PI / 2f - ((float)data.Angle).ToRadians();

                float distance = data.MinDistance + (float)Random.Shared.NextDouble() * (data.MaxDistance - data.MinDistance);
                position = source.Position.GetPoint2D(angle, distance);
            }

            // TODO: this needs to be smarter and take other geometry into account, the same as forced movement
            float? terrainHeight = source.Map.GetTerrainHeight(position.X, position.Z);
            if (position.Y < terrainHeight)
                position.Y = terrainHeight.Value;

            return position;
        }

        /// <summary>
        /// Return <see cref="ICreatureInfo"/> at the level of the summoner if the creature has a level range.
        /// </summary>
        /// <remarks>
        /// Many summoned creatures have a level range (1-99 on 81 SummonCreature creatures, 1-60 on the class pets) that <see cref="ICreatureInfo.GetLevel"/> would pick a random level from.
        /// </remarks>
        private static ICreatureInfo GetSummonCreatureInfo(ICreatureInfo creatureInfo, IUnitEntity summoner)
        {
            if (creatureInfo.Entry.MinLevel == creatureInfo.Entry.MaxLevel)
                return creatureInfo;

            return new CreatureInfoOverride()
                .SetCreatureInfoOverride(creatureInfo)
                .SetLevelOverride(Math.Clamp(summoner.Level, creatureInfo.Entry.MinLevel, creatureInfo.Entry.MaxLevel));
        }
    }
}
