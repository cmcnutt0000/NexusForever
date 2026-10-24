using Microsoft.Extensions.Logging;
using NexusForever.Game.Abstract.Combat;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Combat;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ThreatModification)]
    public class SpellEffectThreatModificationHandler : ISpellEffectApplyHandler<ISpellEffectThreatModificationData>
    {
        #region Dependency Injection

        private readonly ILogger<SpellEffectThreatModificationHandler> log;

        public SpellEffectThreatModificationHandler(
            ILogger<SpellEffectThreatModificationHandler> log)
        {
            this.log = log;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectThreatModificationData data)
        {
            // the threat between the caster and the target is held by whichever isn't a player,
            // eg: Medic Calm lowers the caster's threat on foes and a creature's "Wipe Threat" lowers its own threat on its target
            IUnitEntity caster = executionContext.Spell.Caster;
            (IUnitEntity holder, IUnitEntity hated) = target is IPlayer && caster is not IPlayer ? (caster, target) : (target, caster);

            IHostileEntity hostile = holder.ThreatManager.GetHostile(hated.Guid);
            switch (data.Type)
            {
                case ThreatModificationType.Multiply:
                    hostile?.UpdateThreat(-(int)MathF.Round(hostile.Threat * (1f - data.Multiplier)));
                    break;
                case ThreatModificationType.ReducePercent:
                    hostile?.UpdateThreat(-(int)MathF.Round(hostile.Threat * MathF.Min(data.Multiplier, 100f) / 100f));
                    break;
                case ThreatModificationType.Drop:
                    holder.ThreatManager.RemoveHostile(hated.Guid);
                    break;
                case ThreatModificationType.Add:
                    holder.ThreatManager.UpdateThreat(hated, (int)data.Amount);
                    break;
                case ThreatModificationType.Wipe:
                    holder.ThreatManager.ClearThreatList();
                    break;
                default:
                    log.LogWarning($"Unhandled ThreatModificationType {data.Type}");
                    break;
            }

            return SpellEffectExecutionResult.Ok;
        }
    }
}
