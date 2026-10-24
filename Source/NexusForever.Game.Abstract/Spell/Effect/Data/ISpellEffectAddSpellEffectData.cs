namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// Eg: Elementalist Talent 23 "Your Lightning Strike now stuns targets that are shocked" adds a CCStateSet effect to Lightning Strike (2829) and Shadow Monk Item Talent 1 adds an effect to both Claw Slash base spells. DataBits03 to DataBits05 (1, -1 and 60 on 13 rows) aren't known.
    /// </remarks>
    public interface ISpellEffectAddSpellEffectData : ISpellEffectData
    {
        /// <summary>
        /// Base spell whose casts are changed, 0 to use the spell group.
        /// </summary>
        uint Spell4BaseId { get; }

        /// <summary>
        /// Spell group whose casts are changed when there is no base spell.
        /// </summary>
        uint SpellGroupId { get; }

        /// <summary>
        /// Spell4Effects row added to or removed from the casts.
        /// </summary>
        uint Spell4EffectsId { get; }
    }
}
