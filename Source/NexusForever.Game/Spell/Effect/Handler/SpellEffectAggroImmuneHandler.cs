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
    /// The bearer doesn't generate threat, see <see cref="IUnitEntity.TakeDamage"/>.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.AggroImmune)]
    public class SpellEffectAggroImmuneHandler : SpellEffectCombatEffectHandler<ISpellEffectDefaultData>
    {
    }
}
