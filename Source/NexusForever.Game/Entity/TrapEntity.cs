using System.Numerics;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Entity.Creature;
using NexusForever.Game.Abstract.Entity.Movement;
using NexusForever.Game.Abstract.Map;
using NexusForever.Game.Map.Search;
using NexusForever.Game.Prerequisite;
using NexusForever.Game.Spell;
using NexusForever.Game.Static.Entity;
using NexusForever.Network.World.Entity;
using NexusForever.Network.World.Entity.Model;
using NexusForever.Shared.Game;

namespace NexusForever.Game.Entity
{
    public class TrapEntity : WorldEntity, ITrapEntity
    {
        public override EntityType Type => EntityType.Trap;

        /// <summary>
        /// Guid of the <see cref="IUnitEntity"/> that owns the <see cref="ITrapEntity"/>.
        /// </summary>
        public uint OwnerGuid { get; private set; }

        private uint spell4Id;
        private float radius;
        private uint prerequisiteId;
        private bool enemiesOnly;

        private UpdateTimer armTimer;
        private UpdateTimer lifetimeTimer;
        private bool isArmed;
        private bool isTripped;

        #region Dependency Injection

        public TrapEntity(IMovementManager movementManager,
            IEntitySummonFactory entitySummonFactory)
            : base(movementManager, entitySummonFactory)
        {
        }

        #endregion

        /// <summary>
        /// Initialise <see cref="ITrapEntity"/> for owner with the spell it casts when it is tripped.
        /// </summary>
        /// <remarks>
        /// The trap arms after the arm time and is tripped by the first unit within radius that meets the prerequisite, if enemies only is set it must also be an enemy of the owner.
        /// It is removed when the lifetime ends.
        /// </remarks>
        public void Initialise(IUnitEntity owner, ICreatureInfo creatureInfo, uint spell4Id, float radius, uint prerequisiteId, bool enemiesOnly, TimeSpan armTime, TimeSpan? lifetime)
        {
            OwnerGuid           = owner.Guid;
            this.spell4Id       = spell4Id;
            this.radius         = radius;
            this.prerequisiteId = prerequisiteId;
            this.enemiesOnly    = enemiesOnly;
            armTimer            = new UpdateTimer(armTime);
            lifetimeTimer       = lifetime != null ? new UpdateTimer(lifetime.Value) : null;

            Initialise(creatureInfo);
        }

        protected override IEntityModel BuildEntityModel()
        {
            return new TrapEntityModel
            {
                CreatureId = CreatureId,
                OwnerId    = OwnerGuid
            };
        }

        /// <summary>
        /// Invoked when <see cref="ITrapEntity"/> is added to <see cref="IBaseMap"/>.
        /// </summary>
        public override void OnAddToMap(IBaseMap map, uint guid, Vector3 vector)
        {
            base.OnAddToMap(map, guid, vector);
            SetInRangeCheck(radius);
        }

        /// <summary>
        /// Invoked each world tick with the delta since the previous tick occurred.
        /// </summary>
        public override void Update(double lastTick)
        {
            base.Update(lastTick);

            if (lifetimeTimer != null)
            {
                lifetimeTimer.Update(lastTick);
                if (lifetimeTimer.HasElapsed)
                {
                    RemoveFromMap();
                    return;
                }
            }

            if (!isArmed)
            {
                armTimer.Update(lastTick);
                if (armTimer.HasElapsed)
                {
                    isArmed = true;
                    TripInRange();
                }
            }
        }

        protected override void AddToRange(IGridEntity entity)
        {
            base.AddToRange(entity);

            if (isArmed && entity is IUnitEntity unit)
                Trip(unit);
        }

        /// <summary>
        /// Trip the <see cref="ITrapEntity"/> with a unit that was already within radius when it armed.
        /// </summary>
        private void TripInRange()
        {
            var check = new SearchCheckRange<IUnitEntity>();
            check.Initialise(Position, radius);

            foreach (IUnitEntity unit in Map.Search(Position, radius, check).ToList())
            {
                Trip(unit);
                if (isTripped)
                    break;
            }
        }

        private void Trip(IUnitEntity unit)
        {
            if (isTripped || !unit.IsAlive || unit.Guid == OwnerGuid)
                return;

            IUnitEntity owner = Map.GetEntity<IUnitEntity>(OwnerGuid);
            if (owner == null)
                return;

            // "If an enemy trips the trap, it will detonate" (Proximity Mine)
            if (enemiesOnly && !owner.CanAttack(unit))
                return;

            // Mirage requires an injured party member of the owner
            if (prerequisiteId != 0u && !PrerequisiteManager.Instance.Meets(unit, prerequisiteId, owner))
                return;

            isTripped = true;

            // the trap is the positional unit of the spell: implicit targets are searched around it and the owner unsummons it when the spell finishes
            owner.CastSpell(spell4Id, new SpellParameters
            {
                PrimaryTargetId  = unit.Guid,
                PositionalUnitId = Guid
            });
        }
    }
}
