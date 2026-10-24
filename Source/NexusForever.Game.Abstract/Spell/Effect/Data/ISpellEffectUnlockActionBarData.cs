namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectUnlockActionBarData : ISpellEffectData
    {
        /// <summary>
        /// Index of the action set that is unlocked.
        /// </summary>
        byte ActionSetIndex { get; }
    }
}
