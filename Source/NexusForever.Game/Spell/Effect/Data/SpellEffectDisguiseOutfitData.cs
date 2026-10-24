using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Entity;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectDisguiseOutfitData : ISpellEffectDisguiseOutfitData
    {
        public Spell4EffectsEntry Entry { get; private set; }
        public uint OutfitInfoId { get; private set; }
        public ushort WeaponDisplayId { get; private set; }
        public ushort ItemDisplayId { get; private set; }
        public ItemSlot? ItemDisplaySlot { get; private set; }

        #region Dependency Injection

        private readonly IGameTableManager gameTableManager;

        public SpellEffectDisguiseOutfitData(
            IGameTableManager gameTableManager)
        {
            this.gameTableManager = gameTableManager;
        }

        #endregion

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry           = entry;
            // 154 of the 156 rows with an id are Creature2OutfitInfo rows, eg: 9249 the Dominion hazmat suit, 9274 Phantasmal Armor
            OutfitInfoId    = entry.DataBits00;
            // all 210 rows with an id are ItemDisplay rows of weapons (item slot 20), eg: 2332 a rifle for "CSP Disruptor"
            WeaponDisplayId = (ushort)entry.DataBits01;
            // the 11 rows with an id are ItemDisplay rows, 8 of them in the Engineer mech suit slot (64), eg: 4807 "Mech Suit"
            // dataBits03 (1 in 19 rows), dataBits04 and dataBits05 (1.0 in 4 rows) and dataBits08 (7390 in 4 rows) aren't known
            ItemDisplayId   = (ushort)entry.DataBits02;

            if (ItemDisplayId != 0)
            {
                ItemDisplayEntry displayEntry = gameTableManager.ItemDisplay.GetEntry(ItemDisplayId);
                Item2TypeEntry typeEntry = displayEntry != null ? gameTableManager.Item2Type.GetEntry(displayEntry.Item2TypeId) : null;
                if (typeEntry != null && typeEntry.ItemSlotId != 0u)
                    ItemDisplaySlot = (ItemSlot)typeEntry.ItemSlotId;
            }
        }
    }
}
