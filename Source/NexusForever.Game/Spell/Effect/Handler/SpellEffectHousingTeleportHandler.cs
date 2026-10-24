using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Housing;
using NexusForever.Game.Abstract.Map.Lock;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Shared;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <summary>
    /// Teleports the player to their house, "Recall - House": "Teleports you to your housing property."
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120), the teleport is the server's. It goes to the entrance of the player's residence, made if they have none, as <see cref="SpellEffectTeleportHandler"/> does for housing locations.
    /// The two "Teleport to the Skymap" spells have the same data and go there too (inferred).
    /// TODO: the settler "Report Home" spells (dataBits01 1) and 4 old auto attack spells (dataBits00 2) don't teleport, where they go isn't known.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.HousingTeleport)]
    public class SpellEffectHousingTeleportHandler : ISpellEffectApplyHandler<ISpellEffectHousingTeleportData>
    {
        #region Dependency Injection

        private readonly IGlobalResidenceManager globalResidenceManager;
        private readonly IMapLockManager mapLockManager;

        public SpellEffectHousingTeleportHandler(
            IGlobalResidenceManager globalResidenceManager,
            IMapLockManager mapLockManager)
        {
            this.globalResidenceManager = globalResidenceManager;
            this.mapLockManager         = mapLockManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectHousingTeleportData data)
        {
            if (target is not IPlayer player)
                return SpellEffectExecutionResult.Ok;

            if (data.Unknown00 != 0u || data.Unknown01 != 0u)
                return SpellEffectExecutionResult.Ok;

            if (!player.CanTeleport())
                return SpellEffectExecutionResult.Ok;

            IResidence residence = globalResidenceManager.GetResidenceByOwner(player.Name);
            residence ??= globalResidenceManager.CreateResidence(player);

            IResidenceEntrance entrance = globalResidenceManager.GetResidenceEntrance(residence.PropertyInfoId);
            if (entrance == null)
                return SpellEffectExecutionResult.Ok;

            IMapLock mapLock = mapLockManager.GetResidenceLock(residence.Parent ?? residence);

            player.Rotation = entrance.Rotation.ToEuler();
            player.TeleportTo(entrance.Entry, entrance.Position, mapLock);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
