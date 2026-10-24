using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Quest;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.QuestAdvanceObjective)]
    public class SpellEffectQuestAdvanceObjectiveHandler : ISpellEffectApplyHandler<ISpellEffectQuestAdvanceObjectiveData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectQuestAdvanceObjectiveData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            IQuest quest = player.QuestManager.GetActiveQuest((ushort)data.QuestId);
            if (quest == null)
                return SpellEffectExecutionResult.Ok;

            // 8 effects name an index past the quest's objectives
            IQuestObjective objective = quest.GetQuestObjectiveByIndex(data.ObjectiveIndex);
            if (objective == null)
                return SpellEffectExecutionResult.Ok;

            // TODO: checklist progress is a bit per entry and the effect doesn't name one (2 effects, quest 6978)
            if (objective.ObjectiveInfo.IsChecklist())
                return SpellEffectExecutionResult.Ok;

            quest.ObjectiveUpdate(objective.ObjectiveInfo.Id, data.Count);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
