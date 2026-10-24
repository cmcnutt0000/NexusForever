using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Spell.Target;

namespace NexusForever.Script.Template
{
    public interface ISpellScript
    {
        /// <summary>
        /// Invoked when a Script effect of the spell is applied to <see cref="IUnitEntity"/>.
        /// </summary>
        void OnScriptEffectApply(IUnitEntity target, ISpellTargetEffectInfo info)
        {
        }

        /// <summary>
        /// Invoked when a Script effect of the spell is removed from <see cref="IUnitEntity"/>.
        /// </summary>
        void OnScriptEffectRemove(IUnitEntity target, ISpellTargetEffectInfo info)
        {
        }
    }
}
