using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <remarks>
    /// Checked when the bearer would die, see <see cref="IUnitEntity.ModifyHealth"/>.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.DelayDeath)]
    public class SpellEffectDelayDeathHandler : SpellEffectCombatEffectHandler<ISpellEffectDelayDeathData>
    {
    }
}
