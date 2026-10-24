using NexusForever.Game.Static.Entity;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectItemVisualSwapData : ISpellEffectData
    {
        /// <summary>
        /// Equipment slot whose visual is swapped.
        /// </summary>
        EquippedItem EquippedItem { get; }

        /// <summary>
        /// <see cref="ItemSlot"/> the client shows the visual in, null when it doesn't map the equipment slot to one.
        /// </summary>
        ItemSlot? Slot { get; }

        /// <summary>
        /// ItemDisplay shown in the slot.
        /// </summary>
        ushort DisplayId { get; }
    }
}
