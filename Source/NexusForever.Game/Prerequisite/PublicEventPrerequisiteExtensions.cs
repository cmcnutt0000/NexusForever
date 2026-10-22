using NexusForever.Game.Abstract.PublicEvent;

namespace NexusForever.Game.Prerequisite
{
    public static class PublicEventPrerequisiteExtensions
    {
        /// <summary>
        /// Return the active <see cref="IPublicEvent"/> with the supplied id, finished events aren't active.
        /// </summary>
        public static IPublicEvent GetActiveEvent(this IPublicEventManager manager, uint publicEventId)
        {
            IPublicEvent publicEvent = manager?.GetEvent(publicEventId);
            return publicEvent == null || publicEvent.HasFinished ? null : publicEvent;
        }

        /// <summary>
        /// Return the <see cref="IPublicEventObjective"/> with the supplied id if its public event is active.
        /// </summary>
        public static IPublicEventObjective GetActiveObjective(this IPublicEventManager manager, uint objectiveId)
        {
            foreach (IPublicEvent publicEvent in manager?.GetEvents() ?? Enumerable.Empty<IPublicEvent>())
            {
                if (publicEvent.HasFinished)
                    continue;

                foreach (IPublicEventTeam team in publicEvent.GetTeams())
                {
                    IPublicEventObjective objective = team.GetObjectives().FirstOrDefault(o => o.Entry.Id == objectiveId);
                    if (objective != null)
                        return objective;
                }
            }

            return null;
        }
    }
}
