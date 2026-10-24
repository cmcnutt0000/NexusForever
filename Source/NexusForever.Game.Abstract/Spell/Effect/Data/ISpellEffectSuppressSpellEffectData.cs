namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// Eg: Elementalist Talent 23 removes the incapacitate of Lightning Strike (2829) that its AddSpellEffect replaces with a stun.
    /// </remarks>
    public interface ISpellEffectSuppressSpellEffectData : ISpellEffectData
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
