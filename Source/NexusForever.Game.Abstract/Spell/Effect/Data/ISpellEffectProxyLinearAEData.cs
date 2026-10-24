namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    /// <remarks>
    /// Inferred from the data, the client has no class of its own for it: 368 of 521 rows are 1 point, eg: Stemdragon Spore Bomb at 45 and -45 degrees, 5 and 10 metres; lines of up to 20 points have 10 to 500 milliseconds between them. DataBits05 (12 rows, 45 to 315) isn't known.
    /// </remarks>
    public interface ISpellEffectProxyLinearAEData : ISpellEffectData
    {
        /// <summary>
        /// Spell cast at each point.
        /// </summary>
        uint Spell4Id { get; }

        /// <summary>
        /// Degrees from the facing of the target of the line.
        /// </summary>
        float Angle { get; }

        /// <summary>
        /// Distance of the first point.
        /// </summary>
        float Distance { get; }

        /// <summary>
        /// Number of points.
        /// </summary>
        uint Count { get; }

        /// <summary>
        /// Milliseconds between the points.
        /// </summary>
        uint Interval { get; }

        /// <summary>
        /// Distance between the points, the distance of the first point when 0.
        /// </summary>
        float Spacing { get; }
    }
}
