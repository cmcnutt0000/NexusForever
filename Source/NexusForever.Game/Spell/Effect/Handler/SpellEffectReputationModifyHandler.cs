using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Reputation;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ReputationModify)]
    public class SpellEffectReputationModifyHandler : ISpellEffectApplyHandler<ISpellEffectReputationModifyData>, ISpellEffectRemoveHandler<ISpellEffectReputationModifyData>
    {
        #region Dependency Injection

        private readonly IFactionManager factionManager;

        public SpellEffectReputationModifyHandler(
            IFactionManager factionManager)
        {
            this.factionManager = factionManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectReputationModifyData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            if (factionManager.GetFaction(data.FactionId) == null)
                return SpellEffectExecutionResult.Ok;

            // pheromones, disguises and cages make a faction friendly or hostile while they last, the client is sent an override
            if (data.IsPermanent)
                player.ReputationManager.UpdateReputation(data.FactionId, data.Value);
            else
                player.ReputationManager.AddReputationOverride(data.FactionId, data.Value);

            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectReputationModifyData data)
        {
            if (target is not IPlayer player || data.IsPermanent)
                return;

            player.ReputationManager.RemoveReputationOverride(data.FactionId);
        }
    }
}
