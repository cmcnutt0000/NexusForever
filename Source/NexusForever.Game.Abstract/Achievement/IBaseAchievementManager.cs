using NexusForever.Database.Character;
using NexusForever.Database.Character.Model;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Static.Achievement;

namespace NexusForever.Game.Abstract.Achievement
{
    public interface IBaseAchievementManager<T> : IDatabaseCharacter where T : class, IAchievementModel, new()
    {
        uint AchievementPoints { get; }

        /// <summary>
        /// Initialise a collection of existing achievement database models.
        /// </summary>
        void Initialise(IEnumerable<T> models, bool isPlayer);

        /// <summary>
        /// Returns if the supplied achievement id has been completed.
        /// </summary>
        bool HasCompletedAchievement(ushort id);

        /// <summary>
        /// Return the <see cref="IAchievement"/> with the supplied id, null if there is no progress for it.
        /// </summary>
        IAchievement GetAchievementProgress(ushort id);

        /// <summary>
        /// Send initial <see cref="IAchievement{T}"/> information to owner on login.
        /// </summary>
        void SendInitialPackets(IPlayer target);

        /// <summary>
        /// Grant achievement by supplied achievement id.
        /// </summary>
        void GrantAchievement(ushort id);

        /// <summary>
        /// Update or complete any achievements of <see cref="AchievementType"/> as <see cref="IPlayer"/> with supplied object ids.
        /// </summary>
        void CheckAchievements(IPlayer target, AchievementType type, uint objectId, uint objectIdAlt = 0, uint count = 1);

        /// <summary>
        /// Advance achievement with supplied id by count as <see cref="IPlayer"/>, its object ids aren't checked.
        /// </summary>
        void AdvanceAchievement(IPlayer target, ushort id, uint count = 1);
    }
}