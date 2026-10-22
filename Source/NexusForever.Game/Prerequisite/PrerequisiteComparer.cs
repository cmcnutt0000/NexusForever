using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite
{
    public static class PrerequisiteComparer
    {
        /// <summary>
        /// Compare the unit's value with the prerequisite value, the unit's value is on the left as in the client (0x1404A2090 and 0x1404A2010).
        /// </summary>
        /// <returns>False if the <see cref="PrerequisiteComparison"/> is unknown.</returns>
        public static bool TryCompare<T>(PrerequisiteComparison comparison, T actual, T value, out bool result)
            where T : IComparable<T>
        {
            int order = actual.CompareTo(value);
            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    result = order == 0;
                    return true;
                case PrerequisiteComparison.NotEqual:
                    result = order != 0;
                    return true;
                case PrerequisiteComparison.GreaterThan:
                    result = order > 0;
                    return true;
                case PrerequisiteComparison.GreaterThanOrEqual:
                    result = order >= 0;
                    return true;
                case PrerequisiteComparison.LessThan:
                    result = order < 0;
                    return true;
                case PrerequisiteComparison.LessThanOrEqual:
                    result = order <= 0;
                    return true;
                default:
                    result = false;
                    return false;
            }
        }
    }
}
