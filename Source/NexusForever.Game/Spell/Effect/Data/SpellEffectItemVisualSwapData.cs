using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Static.Entity;
using NexusForever.GameTable.Model;

namespace NexusForever.Game.Spell.Effect.Data
{
    public class SpellEffectItemVisualSwapData : ISpellEffectItemVisualSwapData
    {
        // the client's table at 0x140C39050, indexed by equipment slot, the effect (0x1405D9BE0) shows the visual in this item slot
        // slots 17 to 30 are 0 there and the effect ignores slots from 31 up
        private static readonly ItemSlot[] equippedItemSlots =
        [
            ItemSlot.ArmorChest,
            ItemSlot.ArmorLegs,
            ItemSlot.ArmorHead,
            ItemSlot.ArmorShoulder,
            ItemSlot.ArmorFeet,
            ItemSlot.ArmorHands,
            ItemSlot.WeaponTool,
            ItemSlot.Bag,
            ItemSlot.Bag,
            ItemSlot.Bag,
            ItemSlot.Bag,
            ItemSlot.ArmorGadget,
            ItemSlot.Bag,
            ItemSlot.Bag,
            ItemSlot.Bag,
            ItemSlot.ArmorShields,
            ItemSlot.WeaponPrimary
        ];

        public Spell4EffectsEntry Entry { get; private set; }
        public EquippedItem EquippedItem { get; private set; }
        public ItemSlot? Slot { get; private set; }
        public ushort DisplayId { get; private set; }

        public void Populate(Spell4EffectsEntry entry)
        {
            Entry        = entry;
            // the client reads dataBits00 and dataBits01 only (+0x40 and +0x44 of its effect copy)
            // in 1,528 of the 1,542 rows the ItemDisplay's item type has the item slot this table gives, eg: 0 chest, 16 primary weapon
            EquippedItem = (EquippedItem)entry.DataBits00;
            Slot         = entry.DataBits00 < equippedItemSlots.Length ? equippedItemSlots[entry.DataBits00] : null;
            DisplayId    = (ushort)entry.DataBits01;
        }
    }
}
