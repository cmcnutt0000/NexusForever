namespace NexusForever.Game.Static.Quest
{
    [Flags]
    public enum QuestObjectiveFlags
    {
        None                      = 0x0000,
        FollowsPrevious           = 0x0001, // only shown, and without Sequential only active, while the objectives before it are
        Sequential                = 0x0002, // only active once the required objectives before it are complete
        Hidden                    = 0x0008, // never shown in the objective list
        Optional                  = 0x0020, // not required to achieve the quest
        ShowVirtualItemWhenHidden = 0x0100, // VirtualCollect only, the virtual item is listed even when the objective is hidden
        CountedProgress           = 0x0200, // kill objectives with a count above 1 track a count instead of a percentage
        ShowCount                 = 0x0400, // objectives with a count above 1 are shown as a count instead of a percentage
        HideEarlierVirtualItems   = 0x0800  // while incomplete, virtual items of the objectives before it aren't listed
    }
}
