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

        /// <summary>
        /// Evaluate a has/hasn't condition, <see cref="PrerequisiteComparison.Equal"/> passes when it holds and <see cref="PrerequisiteComparison.NotEqual"/> when it doesn't.
        /// </summary>
        /// <returns>False if the <see cref="PrerequisiteComparison"/> is neither, the client fails those.</returns>
        public static bool TryEvaluate(PrerequisiteComparison comparison, bool condition, out bool result)
        {
            switch (comparison)
            {
                case PrerequisiteComparison.Equal:
                    result = condition;
                    return true;
                case PrerequisiteComparison.NotEqual:
                    result = !condition;
                    return true;
                default:
                    result = false;
                    return false;
            }
        }
    }
}
