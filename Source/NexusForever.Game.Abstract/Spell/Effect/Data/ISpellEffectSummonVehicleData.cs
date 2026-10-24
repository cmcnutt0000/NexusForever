namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectSummonVehicleData : ISpellEffectData
    {
        uint CreatureId { get; }
        uint VehicleId { get; }
    }
}
