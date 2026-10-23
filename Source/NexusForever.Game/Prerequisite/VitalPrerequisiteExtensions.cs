using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Static.Entity;

namespace NexusForever.Game.Prerequisite
{
    public static class VitalPrerequisiteExtensions
    {
        // resource index to vital, see 0x140466520 in the client (unit+0x21C)
        private static readonly Vital[] resourceVitals =
        [
            Vital.Endurance, Vital.Resource1, Vital.Resource2, Vital.Resource3, Vital.Resource4, Vital.Resource5,
            Vital.Resource6, Vital.Dash, Vital.Resource8, Vital.Resource9, Vital.Resource10
        ];

        /// <summary>
        /// Return the current value of the supplied <see cref="Vital"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="IWorldEntity.GetVitalValue(Vital)"/> doesn't cover health or shield, see 0x140466520 in the client.
        /// </remarks>
        public static float GetVital(this IUnitEntity unit, Vital vital)
        {
            switch (vital)
            {
                case Vital.Health:
                    return unit.Health;
                case Vital.ShieldCapacity:
                    return unit.Shield;
                default:
                    return unit.GetVitalValue(vital);
            }
        }

        /// <summary>
        /// Return the maximum value of the supplied <see cref="Vital"/>.
        /// </summary>
        /// <remarks>
        /// See 0x1404667C0 in the client, the maximums are the properties the vital setters clamp to.
        /// </remarks>
        public static float GetVitalMax(this IUnitEntity unit, Vital vital)
        {
            switch (vital)
            {
                case Vital.Health:
                    return unit.MaxHealth;
                case Vital.ShieldCapacity:
                    return unit.MaxShieldCapacity;
                case Vital.Endurance:
                    return unit.GetPropertyValue(Property.ResourceMax0);
                case Vital.Resource1:
                case Vital.KineticEnergy:
                case Vital.Volatility:
                case Vital.Actuator:
                case Vital.Actuator2:
                    return unit.GetPropertyValue(Property.ResourceMax1);
                case Vital.Resource2:
                    return unit.GetPropertyValue(Property.ResourceMax2);
                case Vital.Resource3:
                case Vital.SuitPower:
                    return unit.GetPropertyValue(Property.ResourceMax3);
                case Vital.Resource4:
                case Vital.SpellSurge:
                    return unit.GetPropertyValue(Property.ResourceMax4);
                case Vital.Resource5:
                    return unit.GetPropertyValue(Property.ResourceMax5);
                case Vital.Resource6:
                    return unit.GetPropertyValue(Property.ResourceMax6);
                case Vital.Focus:
                    return unit.GetPropertyValue(Property.BaseFocusPool);
                case Vital.Dash:
                    return unit.GetPropertyValue(Property.ResourceMax7);
                case Vital.InterruptArmor:
                    return -1f;
                default:
                    return 0f;
            }
        }

        /// <summary>
        /// Return the value of the class resource with the supplied index, 0 if the index is out of range.
        /// </summary>
        public static float GetResource(this IUnitEntity unit, uint index)
        {
            return index < resourceVitals.Length ? unit.GetVital(resourceVitals[index]) : 0f;
        }
    }
}
