using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Effect;
using NexusForever.Game.Abstract.Spell.Effect.Data;
using NexusForever.Game.Abstract.Spell.Target;
using NexusForever.Game.Map.Search;
using NexusForever.Game.Static.Spell;
using NexusForever.Game.Static.Spell.Effect;
using NexusForever.Script.Template;

namespace NexusForever.Game.Spell.Effect.Handler
{
    [SpellEffectHandler(SpellEffectType.RavelSignal)]
    public class SpellEffectRavelSignalHandler : ISpellEffectApplyHandler<ISpellEffectRavelSignalData>
    {
        /// <summary>
        /// Handle <see cref="ISpell"/> effect apply on <see cref="IUnitEntity"/> target.
        /// </summary>
        /// <remarks>
        /// The effect signals the server's scripts (Ravel), quest items and encounter mechanics use it to move their script on.
        /// The signal (dataBits01, 3,503 ids) and its value (dataBits02, signed) go to the scripts of the units in its scope and to the map scripts.
        /// </remarks>
        public SpellEffectExecutionResult Apply(ISpellExecutionContext executionContext, IUnitEntity target, ISpellTargetEffectInfo info, ISpellEffectRavelSignalData data)
        {
            IUnitEntity source = executionContext.Spell.Caster;

            foreach (IWorldEntity entity in GetReceivers(target, data))
                entity.InvokeScriptCollection<IWorldEntityScript>(s => s.OnRavelSignal(source, data.SignalId, data.Value));

            target.Map.InvokeScriptCollection<IMapScript>(s => s.OnRavelSignal(source, target, data.Scope, data.SignalId, data.Value));
            return SpellEffectExecutionResult.Ok;
        }

        private static IEnumerable<IWorldEntity> GetReceivers(IUnitEntity target, ISpellEffectRavelSignalData data)
        {
            switch (data.Scope)
            {
                case SpellEffectRavelSignalScope.Target:
                    return [target];
                // 251 of the 259 rows have a radius of 10 to 60 m in dataBits05
                case SpellEffectRavelSignalScope.Range:
                {
                    var check = new SearchCheckRange<IWorldEntity>();
                    check.Initialise(target.Position, data.Radius);
                    return target.Map.Search(target.Position, data.Radius, check).ToList();
                }
                // all 60 rows are warplot guard plugs and boss tokens, dataBits04 is one of 6 WarplotsMap sockets
                case SpellEffectRavelSignalScope.WorldSocket:
                {
                    var check = new SearchCheckRange<IWorldEntity>();
                    check.Initialise(target.Position, null);
                    return target.Map.Search(target.Position, null, check)
                        .Where(e => e.WorldSocketId == data.WorldSocketId)
                        .ToList();
                }
                default:
                    // TODO: the receivers of scopes 4 (969 rows) and 5 (673) aren't known, they are mostly dungeon and public event encounters
                    return [];
            }
        }
    }
}
