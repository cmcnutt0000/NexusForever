namespace NexusForever.Game.Static.Spell
{
    public enum DamageShape
    {
        Circle        = 0,
        Ring          = 1,
        Square        = 2,
        /// <summary>
        /// Two boxes, the second turned by a quarter circle.
        /// </summary>
        Cross         = 3,
        Cone          = 4,
        /// <summary>
        /// A circle missing a slice of Param02 degrees, centred in front.
        /// </summary>
        Pie           = 5,
        Ellipse       = 6,
        Rectangle     = 7,
        LongCone      = 8
    }
}
