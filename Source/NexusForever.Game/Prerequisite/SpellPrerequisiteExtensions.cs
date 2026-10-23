using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Target;

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

        private static IEnumerable<ISpell> GetSpellsAffecting(IUnitEntity unit, IUnitEntity caster)
        {
            foreach (ISpellTargetInfo targetInfo in unit.GetAffectingSpells())
            {
                ISpell spell = targetInfo.Collection.Spell;
                if (caster == null || spell.Caster.Guid == caster.Guid)
                    yield return spell;
            }
        }
    }
}
