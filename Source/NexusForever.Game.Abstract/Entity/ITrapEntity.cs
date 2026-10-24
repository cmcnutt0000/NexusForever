using NexusForever.Game.Abstract.Entity.Creature;

namespace NexusForever.Game.Abstract.Entity
{
    public interface ITrapEntity : IWorldEntity
    {
        /// <summary>
        /// Guid of the <see cref="IUnitEntity"/> that owns the <see cref="ITrapEntity"/>.
        /// </summary>
        uint OwnerGuid { get; }

        /// <summary>
        /// Initialise <see cref="ITrapEntity"/> for owner with the spell it casts when it is tripped.
        /// </summary>
        /// <remarks>
        /// The trap arms after the arm time and is tripped by the first unit within radius that meets the prerequisite, if enemies only is set it must also be an enemy of the owner.
        /// It is removed when the lifetime ends.
        /// </remarks>
        void Initialise(IUnitEntity owner, ICreatureInfo creatureInfo, uint spell4Id, float radius, uint prerequisiteId, bool enemiesOnly, TimeSpan armTime, TimeSpan? lifetime);
    }
}
