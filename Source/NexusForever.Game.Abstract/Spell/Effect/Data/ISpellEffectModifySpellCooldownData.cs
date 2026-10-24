using NexusForever.Game.Static.Spell;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectModifySpellCooldownData : ISpellEffectData
    {
        /// <summary>
        /// Which cooldowns are changed.
        /// </summary>
        SpellCooldownModifierType Type { get; }

        /// <summary>
        /// Id of the base spell, spell, spell group or SpellCoolDown.
        /// </summary>
        uint Id { get; }

        /// <summary>
        /// How the cooldowns are changed.
        /// </summary>
        SpellCooldownModifierOperation Operation { get; }

        /// <summary>
        /// Multiplier or milliseconds.
        /// </summary>
        float Amount { get; }
    }
}
