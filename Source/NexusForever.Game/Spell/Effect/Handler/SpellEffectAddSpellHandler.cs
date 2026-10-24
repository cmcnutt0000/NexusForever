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
    [SpellEffectHandler(SpellEffectType.AddSpell)]
    public class SpellEffectAddSpellHandler : ISpellEffectApplyHandler<ISpellEffectAddSpellData>
    {
        #region Dependency Injection

        private readonly IGameTableManager gameTableManager;

        public SpellEffectAddSpellHandler(
            IGameTableManager gameTableManager)
        {
            this.gameTableManager = gameTableManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectAddSpellData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.PreventEffect;

            Spell4Entry entry = gameTableManager.Spell4.GetEntry(data.Spell4Id);
            if (entry == null)
                return SpellEffectExecutionResult.PreventEffect;

            if (player.SpellManager.GetSpell(entry.Spell4BaseIdBaseSpell) == null)
                player.SpellManager.AddSpell(entry.Spell4BaseIdBaseSpell, (byte)Math.Max(1u, entry.TierIndex));

            return SpellEffectExecutionResult.Ok;
        }
    }
}
