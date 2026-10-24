using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ChangePhase)]
    public class SpellEffectChangePhaseHandler : ISpellEffectApplyHandler<ISpellEffectChangePhaseData>, ISpellEffectRemoveHandler<ISpellEffectChangePhaseData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        /// <remarks>
        /// Each mask is kept where the mask field is set and gets the phase field: (phases &amp; mask) | phase.
        /// The flux portals set both masks to 2 and their exits back to 1, Tunnelweb Hallucinogen adds 0x10 with masks of ~0x10 and Collect CRB only clears 0x800.
        /// </remarks>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectChangePhaseData data)
        {
            // TODO: NexusForever keeps phases for players only and doesn't use them for visibility, only the client is told
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            player.SetPhases(
                (player.PhasesIPerceive & data.PhasesIPerceiveMask) | data.PhasesIPerceive,
                (player.PhasesThatPerceiveMe & data.PhasesThatPerceiveMeMask) | data.PhasesThatPerceiveMe);

            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectChangePhaseData data)
        {
            // 85 effects change the phases for good (the portals in and out of the flux), the other 76 last for a duration or until the spell ends
            if (info.GetDuration() == TimeSpan.Zero)
                return;

            if (target is not IPlayer player)
                return;

            // TODO: the phases from before the effect aren't kept, the ones it added are cleared and phase 1 is the fallback
            player.SetPhases(
                RemovePhases(player.PhasesIPerceive, data.PhasesIPerceive),
                RemovePhases(player.PhasesThatPerceiveMe, data.PhasesThatPerceiveMe));
        }

        private static uint RemovePhases(uint phases, uint removed)
        {
            phases &= ~removed;
            return phases != 0u ? phases : 1u;
        }
    }
}
