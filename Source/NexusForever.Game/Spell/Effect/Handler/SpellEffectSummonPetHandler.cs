using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Entity.Creature;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Entity.Creature;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.SummonPet)]
    public class SpellEffectSummonPetHandler : ISpellEffectApplyHandler<ISpellEffectSummonPetData>, ISpellEffectRemoveHandler<ISpellEffectSummonPetData>
    {
        #region Dependency Injection

        private readonly ICreatureInfoManager creatureInfoManager;

        public SpellEffectSummonPetHandler(
            ICreatureInfoManager creatureInfoManager)
        {
            this.creatureInfoManager = creatureInfoManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        /// <remarks>
        /// The class pets (engineer bots, esper phantoms and geists, stalker clones) and path pets, summoned by the caster at the target.
        /// </remarks>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSummonPetData data)
        {
            ICreatureInfo creatureInfo = creatureInfoManager.GetCreatureInfo(data.CreatureId);
            if (creatureInfo == null)
                return SpellEffectExecutionResult.PreventEffect;

            IUnitEntity owner = executionContext.Spell.Caster;

            // the pets have a level range of 1-60 (38 of the 67 creatures), they take the owner's level
            ICreatureInfo petInfo = creatureInfo;
            if (creatureInfo.Entry.MinLevel != creatureInfo.Entry.MaxLevel)
            {
                petInfo = new CreatureInfoOverride()
                    .SetCreatureInfoOverride(creatureInfo)
                    .SetLevelOverride(Math.Clamp(owner.Level, creatureInfo.Entry.MinLevel, creatureInfo.Entry.MaxLevel));
            }

            // TODO: NexusForever has no combat pets: no pet AI to follow, assist or keep a stance and no ServerPetSpawned for the pet bar,
            // the valid stances it needs aren't in the effect data (dataBits01, 9 values, and dataBits09, 10 values, aren't identified)
            owner.SummonFactory.Summon(petInfo, target.Position, target.Rotation);
            return SpellEffectExecutionResult.Ok;
        }

        /// <summary>
        /// Handle <see cref="ISpell"/> effect remove on <see cref="IUnitEntity"/> target.
        /// </summary>
        public void Remove(ISpell spell, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectSummonPetData data)
        {
            // every pet effect has a duration (75) or lasts until the spell is cancelled (77, the engineer bots)
            if (info.GetDuration() == TimeSpan.Zero)
                return;

            // TODO: pets are matched by creature id, the end of one cast also removes the pets of a later one
            spell.Caster.SummonFactory.UnsummonCreature(data.CreatureId);
        }
    }
}
