using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectChangePhaseData : ISpellEffectChangePhaseData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint PhasesIPerceive { get; private set; }
        public uint PhasesThatPerceiveMe { get; private set; }
        public uint PhasesIPerceiveMask { get; private set; }
        public uint PhasesThatPerceiveMeMask { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry                    = entry;
            PhasesIPerceive          = entry.DataBits00;
            PhasesThatPerceiveMe     = entry.DataBits01;
            PhasesIPerceiveMask      = entry.DataBits02;
            PhasesThatPerceiveMeMask = entry.DataBits03;
        }
    }
}
