namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectPetCastSpellData : ISpellEffectData
    {
        uint PetSpellId { get; }
        uint SpellId { get; }
    }
}
