using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectSummonCreatureData : ISpellEffectSummonCreatureData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint CreatureId { get; private set; }
        public uint Count { get; private set; }
        public uint MinDistance { get; private set; }
        public uint MaxDistance { get; private set; }
        public int Angle { get; private set; }
        public TelegraphDamageEntry TelegraphDamage { get; private set; }
        public uint SpellId { get; private set; }

        #region Dependency Injection

        private readonly IGameTableManager gameTableManager;

        public SpellEffectSummonCreatureData(
            IGameTableManager gameTableManager)
        {
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry           = entry;
            CreatureId      = entry.DataBits00;
            // 2 rows have no count and still summon a single object
            Count           = Math.Max(entry.DataBits02, 1u);
            // 10 rows have the distances the other way round
            MinDistance     = Math.Min(entry.DataBits03, entry.DataBits04);
            MaxDistance     = Math.Max(entry.DataBits03, entry.DataBits04);
            Angle           = (int)entry.DataBits05;
            TelegraphDamage = gameTableManager.TelegraphDamage.GetEntry(entry.DataBits06);
            SpellId         = entry.DataBits08;
        }
    }
}
