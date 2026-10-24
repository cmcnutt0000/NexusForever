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
    [SpellEffectHandler(SpellEffectType.PetCastSpell)]
    public class SpellEffectPetCastSpellHandler : ISpellEffectApplyHandler<ISpellEffectPetCastSpellData>
    {
        #region Dependency Injection

        private readonly ISpellInfoManager spellInfoManager;

        public SpellEffectPetCastSpellHandler(
            ISpellInfoManager spellInfoManager)
        {
            this.spellInfoManager = spellInfoManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        /// <remarks>
        /// The pets summoned by the pet spell cast the spell, the bot commands of the engineer are made of these ([Bot Ability] Barrage casts Artillery Bot - Target finder).
        /// </remarks>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectPetCastSpellData data)
        {
            ISpellInfo petSpellInfo = spellInfoManager.GetSpellInfo(data.PetSpellId);
            if (petSpellInfo == null)
                return SpellEffectExecutionResult.PreventEffect;

            IUnitEntity owner = executionContext.Spell.Caster;
            bool cast = false;

            foreach (uint creatureId in petSpellInfo.Effects
                .Where(e => e.EffectType == SpellEffectType.SummonPet)
                .Select(e => e.DataBits00)
                .Distinct())
            {
                foreach (IWorldEntity pet in owner.GetVisibleCreature<IWorldEntity>(creatureId).ToList())
                {
                    if (pet.SummonerGuid != owner.Guid || pet is not IUnitEntity unit)
                        continue;

                    // the command spells have a target range (30 m for Barrage), the pet spells reach further (1000 m for Target finder)
                    unit.CastSpell(data.SpellId, new SpellParameters
                    {
                        PrimaryTargetId = executionContext.Spell.Parameters.PrimaryTargetId
                    });
                    cast = true;
                }
            }

            return cast ? SpellEffectExecutionResult.Ok : SpellEffectExecutionResult.PreventEffect;
        }
    }
}
