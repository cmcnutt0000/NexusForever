namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectSetBusyData : ISpellEffectData
    {
        /// <summary>
        /// Whether the target is set busy or no longer busy.
        /// </summary>
        bool Busy { get; }

        uint Unknown01 { get; }
    }
}
