using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Entity;
using NexusForever.Game.Static.Spell;
using NexusForever.Network.World.Message.Model;
using NetworkItemVisual = NexusForever.Network.World.Message.Model.Shared.ItemVisual;

namespace NexusForever.Game.Spell.Effect
{
    /// <summary>
    /// Appearance changes of the active MimicDisguise, DisguiseOutfit and ItemVisualSwap effects of a unit, applied over its own display, outfit and item visuals when they are sent.
    /// </summary>
    /// <remarks>
    /// The unit's own appearance is left as it is, so an effect that ends has nothing to put back, its handler only sends the visuals again.
    /// The whole appearance of a mimic goes first, then outfits, then single item slots, and of several effects of a type the last one applied wins; that order is a choice, the client doesn't say.
    /// </remarks>
    public static class SpellEffectVisualOverlay
    {
        /// <summary>
        /// Apply the appearance changes of the unit's active effects to a <see cref="ServerEntityVisualUpdate"/> built from its own visuals.
        /// </summary>
        public static void Apply(IUnitEntity unit, ServerEntityVisualUpdate update)
        {
            uint displayInfo  = update.DisplayInfo;
            ushort outfitInfo = update.OutfitInfo;
            IPlayer mimicked  = Apply(unit, ref displayInfo, ref outfitInfo, update.ItemVisuals);

            update.DisplayInfo = displayInfo;
            update.OutfitInfo  = outfitInfo;

            if (mimicked != null)
            {
                update.Race = (byte)mimicked.Race;
                update.Sex  = (byte)mimicked.Sex;
            }
        }

        /// <summary>
        /// Apply the appearance changes of the unit's active effects to a <see cref="ServerEntityCreate"/> built from its own visuals.
        /// </summary>
        public static void Apply(IUnitEntity unit, ServerEntityCreate entityCreate)
        {
            uint displayInfo  = entityCreate.DisplayInfo;
            ushort outfitInfo = entityCreate.OutfitInfo;
            // TODO: the entity create of a non player unit has no race and sex, a unit mimicking a player shows them after its next visual update
            Apply(unit, ref displayInfo, ref outfitInfo, entityCreate.VisibleItems);

            entityCreate.DisplayInfo = displayInfo;
            entityCreate.OutfitInfo  = outfitInfo;
        }

        private static IPlayer Apply(IUnitEntity unit, ref uint displayInfo, ref ushort outfitInfo, List<NetworkItemVisual> itemVisuals)
        {
            if (unit.CombatEffectManager == null)
                return null;

            // MimicDisguise effects are kept by their caster, the unit takes on the appearance of the effect's target
            IPlayer mimicked = null;
            foreach (ISpellTargetEffectInfo info in unit.CombatEffectManager.GetEffects(SpellEffectType.MimicDisguise))
            {
                IUnitEntity source = info.Target.GetTarget();
                if (source == null || source == unit)
                    continue;

                displayInfo = source.DisplayInfoId;
                outfitInfo  = source.OutfitInfoId;

                itemVisuals.Clear();
                itemVisuals.AddRange(source.GetVisuals().Select(v => v.Build()));

                mimicked = source as IPlayer;
            }

            foreach (ISpellEffectDisguiseOutfitData data in unit.CombatEffectManager.GetEffectData<ISpellEffectDisguiseOutfitData>(SpellEffectType.DisguiseOutfit))
            {
                if (data.OutfitInfoId != 0u)
                    outfitInfo = (ushort)data.OutfitInfoId;
                if (data.WeaponDisplayId != 0)
                    SetVisual(itemVisuals, ItemSlot.WeaponPrimary, data.WeaponDisplayId);
                if (data.ItemDisplaySlot != null)
                    SetVisual(itemVisuals, data.ItemDisplaySlot.Value, data.ItemDisplayId);
            }

            foreach (ISpellEffectItemVisualSwapData data in unit.CombatEffectManager.GetEffectData<ISpellEffectItemVisualSwapData>(SpellEffectType.ItemVisualSwap))
                if (data.Slot != null)
                    SetVisual(itemVisuals, data.Slot.Value, data.DisplayId);

            return mimicked;
        }

        private static void SetVisual(List<NetworkItemVisual> itemVisuals, ItemSlot slot, ushort displayId)
        {
            // the client swaps in the display without a colour set or dyes (0x1405D9BE0 passes 0)
            itemVisuals.RemoveAll(v => v.Slot == slot);
            itemVisuals.Add(new NetworkItemVisual
            {
                Slot      = slot,
                DisplayId = displayId
            });
        }
    }
}
