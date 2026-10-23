using NexusForever.Game.Static.Entity;
using NexusForever.Network.Message;

namespace NexusForever.Network.World.Message.Model
{
    [Message(GameMessageOpcode.ServerVendorItemsUpdated)]
    public class ServerVendorItemsUpdated : IWritable
    {
        public class Category : IWritable
        {
            public uint Index { get; set; }
            public uint LocalisedTextId { get; set; }

            public void Write(GamePacketWriter writer)
            {
                writer.Write(Index);
                writer.Write(LocalisedTextId);
            }
        }

        public class Item : IWritable
        {
            public class ItemExtraCost : IWritable
            {
                public ItemExtraCostType ExtraCostType { get; set; }
                public uint Quantity { get; set; }
                public uint ItemOrCurrencyId { get; set; }

                public void Write(GamePacketWriter writer)
                {
                    writer.Write(ExtraCostType, 3);
                    writer.Write(Quantity);
                    writer.Write(ItemOrCurrencyId);
                }
            }

            public uint Index { get; set; }
            public VendorItemType Type { get; set; }
            /// <summary>
            /// Item2, Spell4 or CurrencyType id depending on <see cref="Type"/>.
            /// </summary>
            public uint ItemId { get; set; }
            /// <summary>
            /// Only read for <see cref="VendorItemType.Currency"/>, the currency the price is paid in.
            /// </summary>
            public uint PriceCurrencyTypeId { get; set; }
            public uint StockCount { get; set; }
            public uint PrerequisiteId { get; set; }
            /// <summary>
            /// Only read for <see cref="VendorItemType.Currency"/>, other types take the price from the game table or the extra costs.
            /// </summary>
            public uint PriceAmount { get; set; }
            public uint CategoryIndex { get; set; }
            public VendorItemFlags Flags { get; set; }
            public ulong RandomCircuitData { get; set; }
            public uint RandomGlyphData { get; set; }
            public ItemExtraCost ExtraCost1 { get; set; }
            public ItemExtraCost ExtraCost2 { get; set; }

            // see 0x140097AE0 (read) and 0x14047ABA0 (stored for Unit:GetVendorItems) in the client
            public void Write(GamePacketWriter writer)
            {
                writer.Write(Index);
                writer.Write(Type, 4);
                writer.Write(ItemId);
                writer.Write(PriceCurrencyTypeId);
                writer.Write(StockCount);
                writer.Write(PrerequisiteId, 17);
                writer.Write(PriceAmount);
                writer.Write(CategoryIndex);
                writer.Write(Flags, 32u);
                writer.Write(RandomCircuitData);
                writer.Write(RandomGlyphData);
                ExtraCost1.Write(writer);
                ExtraCost2.Write(writer);
            }
        }

        public uint Guid { get; set; }
        public List<Category> Categories { get; } = new();
        public List<Item> Items { get; } = new();
        public float SellPriceMultiplier { get; set; }
        public float BuyPriceMultiplier { get; set; }
        /// <summary>
        /// Open the vendor window, ignored unless <see cref="Guid"/> is the unit the player is interacting with.
        /// </summary>
        public bool OpenWindow { get; set; }
        /// <summary>
        /// Replace the vendor's list. Without it and <see cref="OpenWindow"/> a single item updates the entry with the same index.
        /// </summary>
        public bool FullUpdate { get; set; }
        /// <summary>
        /// With <see cref="OpenWindow"/>, ends the interaction instead of opening the window.
        /// </summary>
        public bool CloseWindow { get; set; }

        public void Write(GamePacketWriter writer)
        {
            writer.Write(Guid);

            writer.Write(Categories.Count);
            Categories.ForEach(c => c.Write(writer));
            writer.Write(Items.Count);
            Items.ForEach(i => i.Write(writer));

            writer.Write(SellPriceMultiplier);
            writer.Write(BuyPriceMultiplier);
            writer.Write(OpenWindow);
            writer.Write(FullUpdate);
            writer.Write(CloseWindow);
        }
    }
}
