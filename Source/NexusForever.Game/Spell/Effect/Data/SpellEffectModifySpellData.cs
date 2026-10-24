using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectModifySpellData : ISpellEffectModifySpellData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Spell4BaseId { get; private set; }
        public uint SpellGroupId { get; private set; }
        public IReadOnlyList<Spell4ModificationEntry> Modifications { get; private set; }

        #region Dependency Injection

        private readonly IGameTableManager gameTableManager;

        public SpellEffectModifySpellData(
            IGameTableManager gameTableManager)
        {
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry         = entry;
            Spell4BaseId  = entry.DataBits00;
            SpellGroupId  = entry.DataBits01;
            Modifications = gameTableManager.Spell4Modification.Entries
                .Where(m => m.Spell4EffectsId == entry.Id)
                .ToList();
        }
    }
}
