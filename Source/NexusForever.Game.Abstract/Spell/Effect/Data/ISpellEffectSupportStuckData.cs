namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectSupportStuckData : ISpellEffectData
    {
        /// <summary>
        /// Whether the death costs no durability.
        /// </summary>
        bool IsFree { get; }
    }
}
