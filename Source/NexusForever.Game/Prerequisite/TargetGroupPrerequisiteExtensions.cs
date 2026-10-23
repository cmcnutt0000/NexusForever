using NexusForever.Game.Abstract.Entity;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Prerequisite
{
    public static class TargetGroupPrerequisiteExtensions
    {
        /// <summary>
        /// Return whether the unit is a member of the <see cref="TargetGroupEntry"/> with the supplied id.
        /// </summary>
        /// <remarks>
        /// See 0x1403B4A20 in the client. Odd types match a unit value in the data, the even type after each is its exclusion.
        /// Type 10 needs every listed group to match and type 11 any of them.
        /// </remarks>
        public static bool IsInTargetGroup(this IUnitEntity unit, IGameTableManager gameTableManager, uint targetGroupId, int depth = 0)
        {
            TargetGroupEntry entry = gameTableManager.TargetGroup.GetEntry(targetGroupId);
            if (entry == null || depth > 8)
                return false;

            switch (entry.Type)
            {
                case 1:
                case 2:
                case 9:
                    // type 9 is a second creature id on the client unit (unit+0x140), NexusForever only has the one
                    return IsListed(entry, unit.CreatureId) == (entry.Type != 2);
                case 3:
                case 4:
                    return IsListed(entry, (uint)unit.Faction1) == (entry.Type == 3);
                case 5:
                case 6:
                    return IsListed(entry, unit is IPlayer racePlayer ? (uint)racePlayer.Race : 0u) == (entry.Type == 5);
                case 7:
                case 8:
                    return IsListed(entry, unit is IPlayer classPlayer ? (uint)classPlayer.Class : 0u) == (entry.Type == 7);
                case 10:
                    return entry.DataEntries.Where(id => id != 0u && id != entry.Id)
                        .All(id => unit.IsInTargetGroup(gameTableManager, id, depth + 1));
                case 11:
                    return entry.DataEntries.Where(id => id != 0u && id != entry.Id)
                        .Any(id => unit.IsInTargetGroup(gameTableManager, id, depth + 1));
                case 12:
                case 13:
                    return IsListed(entry, unit.CreatureInfo?.Entry.UnitRaceId ?? 0u) == (entry.Type == 12);
                default:
                    return false;
            }
        }

        private static bool IsListed(TargetGroupEntry entry, uint value)
        {
            return entry.DataEntries.Any(d => d != 0u && d == value);
        }
    }
}
