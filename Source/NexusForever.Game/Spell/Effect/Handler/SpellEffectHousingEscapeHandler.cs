using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Housing;
using NexusForever.Game.Abstract.Map.Instance;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Map.Lock;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Shared;

namespace NexusForever.Game.Spell.Effect.Handler
{
    /// <summary>
    /// Moves the player out to the front of the house on the residence map they are on, spell 41323 "Return to Teleporter" ("Housing Escape - Teleport to safe area in front of house").
    /// </summary>
    /// <remarks>
    /// The client makes a plain effect for this type (0x140718120), the move is the server's.
    /// The safe area is taken to be the residence entrance players arrive at (inferred): the player's own residence when it is on the map, else the residence of the map.
    /// </remarks>
    [SpellEffectHandler(SpellEffectType.HousingEscape)]
    public class SpellEffectHousingEscapeHandler : ISpellEffectApplyHandler<ISpellEffectDefaultData>
    {
        #region Dependency Injection

        private readonly IGlobalResidenceManager globalResidenceManager;

        public SpellEffectHousingEscapeHandler(
            IGlobalResidenceManager globalResidenceManager)
        {
            this.globalResidenceManager = globalResidenceManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectDefaultData data)
        {
            if (target is not IPlayer player || player.Map is not IResidenceMapInstance map)
                return SpellEffectExecutionResult.Ok;

            IResidence residence = player.ResidenceManager.Residence;
            if (residence?.Map != map)
                residence = map.MapLock is IResidenceMapLock mapLock ? globalResidenceManager.GetResidence(mapLock.ResidenceId) : null;

            if (residence == null)
                return SpellEffectExecutionResult.Ok;

            IResidenceEntrance entrance = globalResidenceManager.GetResidenceEntrance(residence.PropertyInfoId);
            if (entrance == null)
                return SpellEffectExecutionResult.Ok;

            player.Rotation = entrance.Rotation.ToEuler();
            player.TeleportToLocal(entrance.Position);
            return SpellEffectExecutionResult.Ok;
        }
    }
}
