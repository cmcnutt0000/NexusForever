using NexusForever.Database.Character;
using NexusForever.Game.Static.Reputation;

namespace NexusForever.Game.Abstract.Reputation
{
    public interface IReputationManager : IDatabaseCharacter, IEnumerable<IReputation>
    {
        /// <summary>
        /// Update <see cref="IReputation"/> for supplied <see cref="Faction"/> and value.
        /// </summary>
        /// <remarks>
        /// Value is a delta, if a negative value is supplied it will be deducted from the existing reputation if any.
        /// </remarks>
        void UpdateReputation(Faction factionId, float value);

        /// <summary>
        /// Get <see cref="IReputation"/> for supplied <see cref="Faction"/>.
        /// </summary>
        IReputation GetReputation(Faction factionId);

        /// <summary>
        /// Temporarily override the reputation for supplied <see cref="Faction"/> with value.
        /// </summary>
        /// <remarks>
        /// The override is read instead of the reputation until it is removed, the reputation itself is unchanged.
        /// </remarks>
        void AddReputationOverride(Faction factionId, float value);

        /// <summary>
        /// Remove the temporary reputation override for supplied <see cref="Faction"/>.
        /// </summary>
        void RemoveReputationOverride(Faction factionId);

        /// <summary>
        /// Return the temporary reputation override for supplied <see cref="Faction"/>, null if there is none.
        /// </summary>
        float? GetReputationOverride(Faction factionId);
    }
}