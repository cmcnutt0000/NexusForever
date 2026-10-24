using NexusForever.Game.Static.Entity;

namespace NexusForever.Game.Abstract.Spell.Effect.Data
{
    public interface ISpellEffectDisguiseOutfitData : ISpellEffectData
    {
        /// <summary>
        /// Creature2OutfitInfo the target wears, 0 leaves its own.
        /// </summary>
        uint OutfitInfoId { get; }

        /// <summary>
        /// ItemDisplay shown as the target's primary weapon, 0 leaves its own.
        /// </summary>
        ushort WeaponDisplayId { get; }

        /// <summary>
        /// ItemDisplay shown in the item slot of its item type, eg: the Engineer mech suit, 0 for none.
        /// </summary>
        ushort ItemDisplayId { get; }

        /// <summary>
        /// <see cref="ItemSlot"/> of <see cref="ItemDisplayId"/>, null if it has none.
        /// </summary>
        ItemSlot? ItemDisplaySlot { get; }
    }
}
