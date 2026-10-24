using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Combat;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Combat;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ThreatTransfer)]
    public class SpellEffectThreatTransferHandler : SpellEffectCombatEffectHandler<ISpellEffectThreatTransferData>
    {
        #region Dependency Injection

        private readonly ILogger<SpellEffectThreatTransferHandler> log;

        public SpellEffectThreatTransferHandler(
            ILogger<SpellEffectThreatTransferHandler> log)
        {
            this.log = log;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectThreatTransferData data)
        {
            IUnitEntity caster = executionContext.Spell.Caster;
            switch (data.Type)
            {
                // the transfer is kept by the unit that generates the threat, see IUnitEntity.TakeDamage
                case ThreatTransferType.BearerToCaster:
                    return base.Apply(executionContext, target, info, data);
                case ThreatTransferType.CasterToBearer:
                    return base.Apply(executionContext, caster, info, data);
                case ThreatTransferType.CasterToSummoner:
                {
                    IUnitEntity summoner = caster.SummonerGuid != null ? target.GetVisible<IUnitEntity>(caster.SummonerGuid.Value) : null;
                    IHostileEntity hostile = target.ThreatManager.GetHostile(caster.Guid);
                    if (summoner == null || hostile == null)
                        return SpellEffectExecutionResult.Ok;

                    int threat = (int)MathF.Round(hostile.Threat * Math.Clamp(data.Fraction, 0f, 1f));
                    hostile.UpdateThreat(-threat);
                    target.ThreatManager.UpdateThreat(summoner, threat);
                    return SpellEffectExecutionResult.Ok;
                }
                default:
                    log.LogWarning($"Unhandled ThreatTransferType {data.Type}");
                    return SpellEffectExecutionResult.Ok;
            }
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public override void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectThreatTransferData data)
        {
            base.Remove(spell, data.Type == ThreatTransferType.CasterToBearer ? spell.Caster : target, info, data);
        }
    }
}
