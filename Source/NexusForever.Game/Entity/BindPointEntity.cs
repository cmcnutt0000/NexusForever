using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Entity.Movement;
using NexusForever.Game.Abstract.Entity.Stat;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.CSI;
using NexusForever.Game.Spell;
using NexusForever.Game.Static.Entity;
using NexusForever.GameTable;
using NexusForever.Network.World.Entity;
using NexusForever.Network.World.Entity.Model;

namespace NexusForever.Game.Entity
{
    /// <remarks>
    /// A transmat terminal. It is a unit so the bind spell's Activate effect can target it.
    /// </remarks>
    public class BindPointEntity : UnitEntity, IBindPointEntity
    {
        public override EntityType Type => EntityType.BindPoint;

        /// <summary>
        /// GameFormula entry holding the spell the client casts to bind (8566, "Bind to Transmatter Terminal").
        /// </summary>
        private const uint BindSpellFormulaId = 472u;

        #region Dependency Injection

        public BindPointEntity(
            IMovementManager movementManager,
            IEntitySummonFactory entitySummonFactory,
            IStatUpdateManager<IUnitEntity> statUpdateManager,
            ISpellFactory spellFactory)
            : base(movementManager, entitySummonFactory, statUpdateManager, spellFactory)
        {
            statUpdateManager.Initialise(this);
        }

        #endregion

        protected override IEntityModel BuildEntityModel()
        {
            return new BindpointEntityModel
            {
                CreatureId = CreatureId
            };
        }

        /// <summary>
        /// Invoked when <see cref="IBindPointEntity"/> is cast activated.
        /// </summary>
        /// <remarks>
        /// Bind points have no Creature2 activate spell. The client casts the GameFormula 472 spell itself and sends
        /// ClientActivateUnitInteraction with the cast's unique id and this unit, see 0x14039C1E0. The spell is a yes/no
        /// client side interaction (175), on yes its Activate effect binds the player in <see cref="OnActivateSuccess"/>.
        /// </remarks>
        public override void OnActivateCast(IPlayer activator, uint interactionId)
        {
            uint spell4Id = GameTableManager.Instance.GameFormula.GetEntry(BindSpellFormulaId)?.Dataint0 ?? 0u;
            if (spell4Id == 0u)
                throw new InvalidOperationException($"GameFormula {BindSpellFormulaId} has no bind spell!");

            var parameters = new SpellParameters
            {
                PrimaryTargetId        = Guid,
                ClientSideInteraction  = new ClientSideInteraction(activator, this, interactionId),
                UserInitiatedSpellCast = true
            };
            activator.CastSpell(spell4Id, parameters);
        }

        /// <summary>
        /// Invoked when <see cref="IBindPointEntity"/>'s activate succeeds.
        /// </summary>
        public override void OnActivateSuccess(IPlayer activator)
        {
            base.OnActivateSuccess(activator);

            if (CreatureInfo.Entry?.BindPointId > 0u)
                activator.SetBindPoint((ushort)CreatureInfo.Entry.BindPointId);
        }
    }
}
