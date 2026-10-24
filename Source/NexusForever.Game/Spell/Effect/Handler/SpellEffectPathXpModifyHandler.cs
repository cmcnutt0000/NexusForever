using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.PathXpModify)]
    public class SpellEffectPathXpModifyHandler : ISpellEffectApplyHandler<ISpellEffectPathXpModifyData>
    {
        #region Dependency Injection

        private readonly IGameTableManager gameTableManager;

        public SpellEffectPathXpModifyHandler(
            IGameTableManager gameTableManager)
        {
            this.gameTableManager = gameTableManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectPathXpModifyData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            // no effect lowers path experience
            if (data.Amount <= 0)
                return SpellEffectExecutionResult.Ok;

            if (!data.IsLevel)
            {
                player.PathManager.AddXp((uint)data.Amount);
                return SpellEffectExecutionResult.Ok;
            }

            IPathEntry pathEntry = player.PathManager.SingleOrDefault(p => p.Path == player.Path);
            if (pathEntry == null)
                return SpellEffectExecutionResult.Ok;

            // grant the experience of the level the amount above the current one, at most the highest level
            uint level = player.PathManager.GetLevel(player.Path) + (uint)data.Amount;
            PathLevelEntry levelEntry = gameTableManager.PathLevel.Entries
                .Where(e => e.PathTypeEnum == (uint)player.Path && e.PathLevel <= level)
                .MaxBy(e => e.PathLevel);
            if (levelEntry == null || levelEntry.PathXP <= pathEntry.TotalXp)
                return SpellEffectExecutionResult.Ok;

            player.PathManager.AddXp(levelEntry.PathXP - pathEntry.TotalXp);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
