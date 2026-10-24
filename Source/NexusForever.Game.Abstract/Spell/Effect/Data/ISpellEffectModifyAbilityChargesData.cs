namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// With DataBits02 1 the client adds the charges to its maximum charges of the base spell, see 0x1405A71B0 in the client, eg: "Increases the charge count of $(spell=34750) to 2"; the other rows give charges back, eg: Warrior Onslaught "Reset Assault and Support ability cooldowns".
    /// </remarks>
    public interface ISpellEffectModifyAbilityChargesData : ISpellEffectData
    {
        /// <summary>
        /// Base spell whose charges change.
        /// </summary>
        uint Spell4BaseId { get; }

        /// <summary>
        /// Number of charges.
        /// </summary>
        int Charges { get; }

        /// <summary>
        /// Raise the maximum charges while the effect is active instead of giving charges back.
        /// </summary>
        bool MaximumCharges { get; }
    }
}
