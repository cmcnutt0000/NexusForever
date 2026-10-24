using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <summary>
    /// The Explorer's "Searching" spells (1s, 2s and 3s casts), they look for the clue of a scavenger hunt (inferred from ClientPathExplorerCastSearching).
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120) and tests for no effect of it, the work is the server's.
    /// The client asks for the search with ClientPathExplorerCastSearching, which carries the band of the search radius (which of the spells) and the PathExplorerScavengerClue.
    /// TODO: NexusForever handles neither that message nor scavenger hunt clues, so nothing is searched for.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.PathActionExplorerDig)]
    public class SpellEffectPathActionExplorerDigHandler : ISpellEffectApplyHandler<ISpellEffectDefaultData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDefaultData data)
        {
            return SpellEffectExecutionResult.Ok;
        }
    }
}
