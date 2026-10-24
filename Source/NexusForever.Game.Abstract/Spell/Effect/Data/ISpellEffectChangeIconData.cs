namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectChangeIconData : ISpellEffectData
    {
        /// <summary>
        /// Spell4 whose icon changes.
        /// </summary>
        uint Spell4Id { get; }

        /// <summary>
        /// Spell4 whose icon is shown instead.
        /// </summary>
        uint IconSpell4Id { get; }
    }
}
