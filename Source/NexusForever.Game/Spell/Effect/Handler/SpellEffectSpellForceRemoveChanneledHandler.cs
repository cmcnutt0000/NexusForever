using Microsoft.Extensions.Logging;
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
    /// Same data as SpellForceRemove, 175 effects use type 3 (base spell), eg: dropping the beacon of "Protective Shield" removes the carrier auras.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.SpellForceRemoveChanneled)]
    public class SpellEffectSpellForceRemoveChanneledHandler : SpellEffectSpellForceRemoveHandler
    {
        public SpellEffectSpellForceRemoveChanneledHandler(
            ILogger<SpellEffectSpellForceRemoveHandler> log)
            : base(log)
        {
        }
    }
}
