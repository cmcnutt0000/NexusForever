namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectChangePhaseData : ISpellEffectData
    {
        uint PhasesIPerceive { get; }
        uint PhasesThatPerceiveMe { get; }
        uint PhasesIPerceiveMask { get; }
        uint PhasesThatPerceiveMeMask { get; }
    }
}
