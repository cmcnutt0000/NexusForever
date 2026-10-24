namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectHousingPlantSeedData : ISpellEffectData
    {
        /// <summary>
        /// Plant that is sown, see the data class.
        /// </summary>
        uint SeedId { get; }
    }
}
