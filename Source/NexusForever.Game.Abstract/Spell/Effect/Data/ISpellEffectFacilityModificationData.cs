namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectFacilityModificationData : ISpellEffectData
    {
        /// <summary>
        /// Facility given to or taken from the target.
        /// </summary>
        uint FacilityId { get; }

        int Unknown01 { get; }

        /// <summary>
        /// What is done with the facility, see the data class.
        /// </summary>
        uint Operation { get; }
    }
}
