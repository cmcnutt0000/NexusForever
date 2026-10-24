using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.Disembark)]
    public class SpellEffectDisembarkHandler : ISpellEffectApplyHandler<ISpellEffectDisembarkData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        /// <remarks>
        /// Generic Dismount Spell (for vehicles), Eject and Abandon Ship have both flags, the power map pickups only <see cref="SpellEffectDisembarkFlags.Mount"/>.
        /// </remarks>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDisembarkData data)
        {
            if (target is not IPlayer player || player.PlatformGuid == null)
                return SpellEffectExecutionResult.Ok;

            if (player.GetVisible<IWorldEntity>(player.PlatformGuid.Value) is not IVehicleEntity vehicle)
                return SpellEffectExecutionResult.Ok;

            bool isMount = vehicle is IMountEntity;
            if ((data.Flags & (isMount ? SpellEffectDisembarkFlags.Mount : SpellEffectDisembarkFlags.Vehicle)) == 0)
                return SpellEffectExecutionResult.Ok;

            // finish the summoning spell like a disembark from the client, its remove handler dismounts
            foreach (ISpell spell in player.GetSpellsByEffect(isMount ? SpellEffectType.SummonMount : SpellEffectType.SummonVehicle).ToList())
                spell.Finish();

            player.Dismount();
            return SpellEffectExecutionResult.Ok;
        }
    }
}
