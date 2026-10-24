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
    /// The bearer's shields don't absorb damage or recover, "Overload: Shields Disabled".
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.ShieldOverload)]
    public class SpellEffectShieldOverloadHandler : SpellEffectCombatEffectHandler<ISpellEffectDefaultData>
    {
    }
}
