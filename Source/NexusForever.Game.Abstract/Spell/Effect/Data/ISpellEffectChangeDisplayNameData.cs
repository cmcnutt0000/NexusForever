namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectChangeDisplayNameData : ISpellEffectData
    {
        /// <summary>
        /// Localised text of the name the target shows.
        /// </summary>
        uint LocalizedTextId { get; }

        uint Unknown02 { get; }
    }
}
