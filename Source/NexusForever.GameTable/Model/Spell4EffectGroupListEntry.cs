namespace NexusForever.GameTable.Model
{
    public class Spell4EffectGroupListEntry
    {
        public uint Id;
        [GameTableFieldArray(32u)]
        public uint[] Spell4EffectGroupIds;
    }
}
