using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Prerequisite
{
    public static class SpellPrerequisiteExtensions
    {
        /// <summary>
        /// Return whether a spell with the supplied Spell4 id is affecting the unit, if a caster is supplied only its spells count.
        /// </summary>
        public static bool IsAffectedBySpell(this IUnitEntity unit, uint spell4Id, IUnitEntity caster = null)
        {
            return GetSpellsAffecting(unit, caster).Any(s => s.Spell4Id == spell4Id);
        }

        /// <summary>
        /// Return whether a spell matching the supplied predicate is affecting the unit, if a caster is supplied only its spells count.
        /// </summary>
        public static bool IsAffectedBySpell(this IUnitEntity unit, Func<ISpell, bool> predicate, IUnitEntity caster = null)
        {
            return GetSpellsAffecting(unit, caster).Any(predicate);
        }

        /// <summary>
        /// Return whether a spell in the supplied spell group is affecting the unit, if a caster is supplied only its spells count.
        /// </summary>
        public static bool IsAffectedBySpellGroup(this IUnitEntity unit, uint spellGroupId, IUnitEntity caster = null)
        {
            return GetSpellsAffecting(unit, caster).Any(s => s.Parameters.SpellInfo.SpellGroups.Contains(spellGroupId));
        }

        /// <summary>
        /// Return the stack count of spells with the supplied Spell4 id affecting the unit, if a caster is supplied only its spells count.
        /// </summary>
        /// <remarks>
        /// The client sums the stack count of each aura, NexusForever sends every spell target with an instance count of 1.
        /// </remarks>
        public static uint GetAffectingSpellStackCount(this IUnitEntity unit, uint spell4Id, IUnitEntity caster = null)
        {
            return (uint)GetSpellsAffecting(unit, caster).Count(s => s.Spell4Id == spell4Id);
        }

        /// <summary>
        /// Return whether an effect matching the supplied predicate is active on the unit, if a caster is supplied only its spells count.
        /// </summary>
        /// <remarks>
        /// The client keeps the active effects in a list on each unit, delayed effects are added as soon as the client is told about them, see 0x1405D6880 in the client.
        /// </remarks>
        public static bool IsAffectedBySpellEffect(this IUnitEntity unit, Func<Spell4EffectsEntry, bool> predicate, IUnitEntity caster = null)
        {
            return GetTargetInfosAffecting(unit, caster)
                .SelectMany(t => t.GetEffects())
                .Any(e => !e.IsFinalised && predicate(e.Entry));
        }

        private static IEnumerable<ISpell> GetSpellsAffecting(IUnitEntity unit, IUnitEntity caster)
        {
            return GetTargetInfosAffecting(unit, caster).Select(t => t.Collection.Spell);
        }

        private static IEnumerable<ISpellTargetInfo> GetTargetInfosAffecting(IUnitEntity unit, IUnitEntity caster)
        {
            foreach (ISpellTargetInfo targetInfo in unit.GetAffectingSpells())
                if (caster == null || targetInfo.Collection.Spell.Caster.Guid == caster.Guid)
                    yield return targetInfo;
        }
    }
}
