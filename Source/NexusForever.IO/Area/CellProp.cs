namespace NexusForever.IO.Area
{
    /// <summary>
    /// Unique ids of the area <see cref="PropEntry"/>s that touch the cell.
    /// </summary>
    public class CellProp : IReadable
    {
        public List<uint> UniqueIds { get; } = new();

        public void Read(BinaryReader reader)
        {
            while (reader.BaseStream.Position + 4 <= reader.BaseStream.Length)
                UniqueIds.Add(reader.ReadUInt32());
        }
    }
}
