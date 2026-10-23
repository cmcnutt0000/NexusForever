using NexusForever.Game.Configuration.Model;
using NexusForever.Shared.Configuration;

namespace NexusForever.Game
{
    public static class InGameTime
    {
        /// <summary>
        /// Return the length of an in game day, live servers used 3.5 hours.
        /// </summary>
        public static TimeSpan LengthOfDay
        {
            get
            {
                uint lengthOfInGameDayInSeconds = SharedConfiguration.Instance.Get<RealmConfig>().LengthOfInGameDay;
                return lengthOfInGameDayInSeconds != 0u ? TimeSpan.FromSeconds(lengthOfInGameDayInSeconds) : TimeSpan.FromHours(3.5d);
            }
        }

        /// <summary>
        /// Return the current in game time of day.
        /// </summary>
        public static TimeSpan TimeOfDay => TimeSpan.FromDays(DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds / LengthOfDay.TotalSeconds % 1);
    }
}
