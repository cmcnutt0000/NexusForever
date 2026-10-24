using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Info;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ActivateSpellCooldown)]
    public class SpellEffectActivateSpellCooldownHandler : ISpellEffectApplyHandler<ISpellEffectActivateSpellCooldownData>
    {
        #region Dependency Injection

        private readonly ISpellInfoManager spellInfoManager;

        public SpellEffectActivateSpellCooldownHandler(
            ISpellInfoManager spellInfoManager)
        {
            this.spellInfoManager = spellInfoManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectActivateSpellCooldownData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.PreventEffect;

            // the tier the player knows, or the first
            ISpellInfo spellInfo = player.SpellManager.GetSpell(data.Spell4BaseId)?.SpellInfo
                ?? spellInfoManager.GetSpellBaseInfo(data.Spell4BaseId)?.GetSpellInfo(1);
            if (spellInfo == null || spellInfo.Entry.SpellCoolDown == 0u)
                return SpellEffectExecutionResult.Ok;

            player.SpellManager.SetSpellCooldown(spellInfo, spellInfo.Entry.SpellCoolDown / 1000d, true);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
