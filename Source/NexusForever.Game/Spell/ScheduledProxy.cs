using System.Numerics;
using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Entity.Creature;
using NexusForever.Game.Abstract.Spell;
using NexusForever.Game.Abstract.Spell.Event;
using NexusForever.Game.Spell.Event;

namespace NexusForever.Game.Spell
{
    /// <summary>
    /// Proxy that casts spells after a delay each and, optionally, at a position each.
    /// </summary>
    /// <remarks>
    /// Used by the proxy effects without a prerequisite of their own: ProxyRandomExclusive, ProxyChannel, ProxyChannelVariableTime and ProxyLinearAE.
    /// </remarks>
    public class ScheduledProxy : IProxy
    {
        /// <summary>
        /// Spell cast by a <see cref="ScheduledProxy"/>, a spell with a position is centred on a positional unit created from the supplied <see cref="ICreatureInfo"/>.
        /// </summary>
        public record ProxySpell(uint Spell4Id, double Delay, Vector3? Position = null, ICreatureInfo PositionalUnit = null);

        public IUnitEntity Target { get; }
        public ISpell ParentSpell { get; }
        public bool CanCast => true;

        private readonly ISpellParameters parentParameters;
        private readonly List<ProxySpell> spells;

        public ScheduledProxy(IUnitEntity target, ISpell parentSpell, ISpellParameters parameters, IEnumerable<ProxySpell> spells)
        {
            Target           = target;
            ParentSpell      = parentSpell;
            parentParameters = parameters;
            this.spells      = spells.ToList();
        }

        public void Evaluate()
        {
        }

        public void Cast(IUnitEntity caster, ISpellEventManager events)
        {
            foreach (ProxySpell spell in spells)
            {
                if (spell.Delay <= 0d)
                    CastSpell(caster, spell);
                else
                    events.EnqueueEvent(new SpellEvent(spell.Delay / 1000d, () => CastSpell(caster, spell)));
            }
        }

        private void CastSpell(IUnitEntity caster, ProxySpell spell)
        {
            var parameters = new SpellParameters
            {
                ParentSpellInfo        = parentParameters.SpellInfo,
                RootSpellInfo          = parentParameters.RootSpellInfo,
                PrimaryTargetId        = Target.Guid,
                UserInitiatedSpellCast = parentParameters.UserInitiatedSpellCast,
                IsProxy                = true
            };

            if (spell.Position == null || spell.PositionalUnit == null)
            {
                caster.CastSpell(spell.Spell4Id, parameters);
                return;
            }

            // the spell is centred on its positional unit and unsummons it when it finishes
            caster.SummonFactory.Summon(spell.PositionalUnit, spell.Position.Value, caster.Rotation, (map, guid, position) =>
            {
                parameters.PositionalUnitId = guid;
                caster.CastSpell(spell.Spell4Id, parameters);
            });
        }
    }
}
