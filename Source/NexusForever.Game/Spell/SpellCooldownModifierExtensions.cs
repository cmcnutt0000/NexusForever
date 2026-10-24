using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Info;
using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Spell
{
    public static class SpellCooldownModifierExtensions
    {
        /// <summary>
        /// Return whether the ModifySpellCooldown effect changes the cooldown of the supplied <see cref="ISpellInfo"/>, see 0x14046B920 in the client.
        /// </summary>
        public static bool Matches(this ISpellEffectModifySpellCooldownData data, ISpellInfo spellInfo)
        {
            return data.Type switch
            {
                SpellCooldownModifierType.BaseSpell  => spellInfo.BaseInfo.Entry.Id == data.Id,
                SpellCooldownModifierType.Spell      => spellInfo.Entry.Id == data.Id,
                SpellCooldownModifierType.SpellGroup => spellInfo.SpellGroups.Contains(data.Id),
                SpellCooldownModifierType.Cooldown   => spellInfo.Cooldowns.Any(c => c.Id == data.Id),
                _                                    => false
            };
        }

        /// <summary>
        /// Return the supplied cooldown in milliseconds changed by the ModifySpellCooldown effect, see 0x14046B920 in the client.
        /// </summary>
        public static double Apply(this ISpellEffectModifySpellCooldownData data, double milliseconds)
        {
            return data.Operation switch
            {
                SpellCooldownModifierOperation.Reduce   => Math.Min(milliseconds, milliseconds * data.Amount),
                SpellCooldownModifierOperation.Multiply => milliseconds * data.Amount,
                SpellCooldownModifierOperation.Add      => Math.Max(0d, milliseconds + data.Amount),
                SpellCooldownModifierOperation.Set      => data.Amount,
                _                                       => milliseconds
            };
        }
    }
}
