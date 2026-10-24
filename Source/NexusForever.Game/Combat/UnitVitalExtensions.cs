using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Combat
{
    public static class UnitVitalExtensions
    {
        /// <summary>
        /// Add the supplied amount, which can be negative, to a <see cref="Vital"/> of the <see cref="IUnitEntity"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="IWorldEntity.ModifyVital(Vital, float)"/> doesn't cover health or shield, health goes through <see cref="IUnitEntity.ModifyHealth"/> so it can kill.
        /// </remarks>
        public static void AddVital(this IUnitEntity unit, Vital vital, float amount, IUnitEntity source)
        {
            switch (vital)
            {
                case Vital.Health:
                {
                    // the damage type only tells healing from damage
                    uint value = (uint)MathF.Round(MathF.Abs(amount));
                    unit.ModifyHealth(value, amount >= 0f ? DamageType.Heal : DamageType.Physical, source);
                    break;
                }
                case Vital.ShieldCapacity:
                    unit.Shield = (uint)Math.Clamp(unit.Shield + MathF.Round(amount), 0f, unit.MaxShieldCapacity);
                    break;
                default:
                    unit.ModifyVital(vital, amount);
                    break;
            }
        }
    }
}
