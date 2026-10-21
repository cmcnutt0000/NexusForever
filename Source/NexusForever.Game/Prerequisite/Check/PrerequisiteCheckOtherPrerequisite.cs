using NexusForever.Game.Abstract.Entity;
using NexusForever.Game.Abstract.Prerequisite;
using NexusForever.Game.Static.Prerequisite;

namespace NexusForever.Game.Prerequisite.Check
{
    [PrerequisiteCheck(PrerequisiteType.OtherPrerequisite)]
    public class PrerequisiteCheckOtherPrerequisite : IPrerequisiteCheck
    {
        #region Dependency Injection

        private readonly IPrerequisiteManager prerequisiteManager;

        public PrerequisiteCheckOtherPrerequisite(
            IPrerequisiteManager prerequisiteManager)
        {
            this.prerequisiteManager = prerequisiteManager;
        }

        #endregion

        public bool Meets(IUnitEntity subject, PrerequisiteComparison comparison, uint value, uint objectId, IPrerequisiteParameters parameters)
        {
            // the client only negates for NotEqual, any other comparison (including 0, which is used by the game tables) checks the prerequisite as is
            // see 0x1404A45DF in the client
            if (comparison == PrerequisiteComparison.NotEqual)
                return !prerequisiteManager.Meets(subject, objectId, parameters);

            return prerequisiteManager.Meets(subject, objectId, parameters);
        }
    }
}
