using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectRavelSignalData : ISpellEffectRavelSignalData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public SpellEffectRavelSignalScope Scope { get; private set; }
        public uint SignalId { get; private set; }
        public int Value { get; private set; }
        public uint WorldSocketId { get; private set; }
        public float Radius { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry         = entry;
            Scope         = (SpellEffectRavelSignalScope)entry.DataBits00;
            SignalId      = entry.DataBits01;
            Value         = (int)entry.DataBits02;
            WorldSocketId = entry.DataBits04;
            Radius        = BitConverter.UInt32BitsToSingle(entry.DataBits05);
        }
    }
}
