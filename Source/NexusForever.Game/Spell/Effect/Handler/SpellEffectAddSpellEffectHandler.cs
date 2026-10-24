using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <remarks>
    /// Read from the caster's effects when it casts, see Spell.Execute.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.AddSpellEffect)]
    public class SpellEffectAddSpellEffectHandler : SpellEffectCombatEffectHandler<ISpellEffectAddSpellEffectData>
    {
    }
}
