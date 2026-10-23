namespace NexusForever.IO.Area
{
    /// <summary>
    /// Props placed in the area, including those of neighbouring areas that reach into it.
    /// </summary>
    /// <remarks>
    /// See 0x1402DF980 in the client, a count followed by <see cref="PropEntry"/>s. A prop is created by the area in <see cref="PropEntry.UniqueId"/>, so the same prop can be listed by several areas.
    /// </remarks>
    public class Prop : IReadable
    {
        public List<PropEntry> Entries { get; } = new();

        public void Read(BinaryReader reader)
        {
            uint count = reader.ReadUInt32();
            if (reader.BaseStream.Length - 4 < count * PropEntry.Size)
                throw new InvalidDataException();

            for (uint i = 0u; i < count; i++)
            {
                var entry = new PropEntry();
                entry.Read(reader);
                Entries.Add(entry);
            }

            foreach (PropEntry entry in Entries)
                entry.ReadStrings(reader);
        }
    }
}
