using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Spell;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectModifySpellEffectData : ISpellEffectModifySpellEffectData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint Spell4EffectsId { get; private set; }
        public uint ListId { get; private set; }
        public bool AllSpells { get; private set; }
        public uint SpellGroupId { get; private set; }
        public SpellEffectType EffectType { get; private set; }
        public IReadOnlyList<Spell4EffectModificationEntry> Modifications { get; private set; }

        #region Dependency Injection

        private readonly IGameTableManager gameTableManager;

        public SpellEffectModifySpellEffectData(
            IGameTableManager gameTableManager)
        {
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry           = entry;
            Spell4EffectsId = entry.DataBits00;
            ListId          = entry.DataBits01;
            AllSpells       = entry.DataBits02 != 0u;
            SpellGroupId    = entry.DataBits03;
            EffectType      = (SpellEffectType)entry.DataBits04;
            Modifications   = gameTableManager.Spell4EffectModification.Entries
                .Where(m => m.Spell4EffectsId == entry.Id)
                .ToList();
        }
    }
}
