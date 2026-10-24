namespace NexusForever.Game.Static.Spell
{
    /// <summary>
    /// Operation of a Spell4Modification or Spell4EffectModification row, see 0x1403D6D70 and 0x1403D6F20 in the client.
    /// </summary>
    public enum SpellModificationType
    {
        /// <summary>
        /// Change by (Data00 - 1) * base value + Data01, up to Data02, or down to Data02 when Data03 is 1.
        /// </summary>
        Change           = 0,

        /// <summary>
        /// Change by (Data00 - 1) * current value, up to Data01, or down to Data01 when Data02 is 1.
        /// </summary>
        ChangeOfCurrent  = 1,

        /// <summary>
        /// Clamp the summed change of the rows before it to at most Data00, or at least Data00 when Data01 is 1.
        /// </summary>
        Clamp            = 2,

        /// <summary>
        /// Set the value to Data00.
        /// </summary>
        Set              = 3,

        /// <summary>
        /// Clear the bits in Data00, or set them when Data01 is 1.
        /// </summary>
        Bits             = 4
    }
}
