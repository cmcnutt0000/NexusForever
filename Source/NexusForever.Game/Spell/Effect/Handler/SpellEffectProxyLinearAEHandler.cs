using System.Numerics;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Entity.Creature;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.GameTable;
using NexusForever.GameTable.Model;
using NexusForever.Shared;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.ProxyLinearAE)]
    public class SpellEffectProxyLinearAEHandler : ISpellEffectApplyHandler<ISpellEffectProxyLinearAEData>
    {
        #region Dependency Injection

        private readonly IGameTableManager gameTableManager;
        private readonly ICreatureInfoManager creatureInfoManager;

        public SpellEffectProxyLinearAEHandler(
            IGameTableManager gameTableManager,
            ICreatureInfoManager creatureInfoManager)
        {
            this.gameTableManager    = gameTableManager;
            this.creatureInfoManager = creatureInfoManager;
        }

        #endregion

        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectProxyLinearAEData data)
        {
            Spell4Entry spellEntry = gameTableManager.Spell4.GetEntry(data.Spell4Id);
            if (spellEntry == null)
                return SpellEffectExecutionResult.PreventEffect;

            // 507 of the 512 spells it casts have a positional unit, each point gets its own
            Spell4BaseEntry baseEntry = gameTableManager.Spell4Base.GetEntry(spellEntry.Spell4BaseIdBaseSpell);
            ICreatureInfo positionalUnit = baseEntry != null && baseEntry.Creature2IdPositionalAoe != 0u
                ? creatureInfoManager.GetCreatureInfo(baseEntry.Creature2IdPositionalAoe)
                : null;

            // the angle is in degrees from the target's facing, the same as SummonCreature
            float angle   = -target.Rotation.X - MathF.PI / 2f - data.Angle.ToRadians();
            float spacing = data.Spacing > 0f ? data.Spacing : data.Distance;

            var spells = new List<ScheduledProxy.ProxySpell>();
            for (uint i = 0u; i < data.Count; i++)
            {
                Vector3 position = target.Position.GetPoint2D(angle, data.Distance + spacing * i);

                // TODO: this needs to be smarter and take other geometry into account, the same as forced movement
                float? terrainHeight = target.Map?.GetTerrainHeight(position.X, position.Z);
                if (position.Y < terrainHeight)
                    position.Y = terrainHeight.Value;

                spells.Add(new ScheduledProxy.ProxySpell(data.Spell4Id, (double)data.Interval * i, position, positionalUnit));
            }

            executionContext.AddProxy(new ScheduledProxy(target, executionContext.Spell, executionContext.Spell.Parameters, spells));
            return SpellEffectExecutionResult.Ok;
        }
    }
}
