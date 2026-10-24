using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.TitleRevoke)]
    public class SpellEffectTitleRevokeHandler : ISpellEffectApplyHandler<ISpellEffectTitleRevokeData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectTitleRevokeData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            if (player.TitleManager.HasTitle((ushort)data.TitleId))
                player.TitleManager.RevokeTitle((ushort)data.TitleId);

            return SpellEffectExecutionResult.Ok;
        }
    }
}
