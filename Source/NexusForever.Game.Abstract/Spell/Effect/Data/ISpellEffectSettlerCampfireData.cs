namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectSettlerCampfireData : ISpellEffectData
    {
        /// <summary>
        /// Tier of the campfire, 0 to 2.
        /// </summary>
        uint Tier { get; }

        uint Unknown01 { get; }
        uint Unknown02 { get; }
    }
}
