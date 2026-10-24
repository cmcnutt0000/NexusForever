using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <remarks>
    /// The units with the effect of one cast share their health, see <see cref="IUnitEntity.TakeDamage"/>, eg: "Sharing health with nearby Acolytes" and Medic Barrier.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.SharedHealthPool)]
    public class SpellEffectSharedHealthPoolHandler : SpellEffectCombatEffectHandler<ISpellEffectDefaultData>
    {
    }
}
