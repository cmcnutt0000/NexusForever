namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectNpcForceFacingData : ISpellEffectData
    {
        /// <summary>
        /// Angle in degrees the bearer turns by, eg: 270, 180, 135 and -90.
        /// </summary>
        float Angle { get; }
    }
}
