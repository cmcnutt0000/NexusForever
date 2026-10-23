using System.Numerics;

namespace NexusForever.IO.Area
{
    /// <summary>
    /// A 104 byte prop placement from the area PROp chunk.
    /// </summary>
    public class PropEntry
    {
        public const int Size = 0x68;

        /// <summary>
        /// Bits 0-6 are the grid x and bits 7-13 the grid y of the area that owns the prop, see 0x1402E0480 in the client.
        /// </summary>
        public uint UniqueId { get; private set; }
        public uint OwnerGridX => UniqueId & 0x7F;
        public uint OwnerGridY => (UniqueId >> 7) & 0x7F;

        /// <summary>
        /// Unique id of the prop this one is attached to, 0 if none.
        /// </summary>
        /// <remarks>
        /// <see cref="Position"/>, <see cref="Rotation"/> and <see cref="Scale"/> are then relative to the parent:
        /// world position = parent position + parent rotation * (position * parent scale). In Eastern and Western that puts
        /// 10,386 of 10,395 attached props inside their cell range.
        /// </remarks>
        public uint ParentUniqueId { get; private set; }
        public uint Unknown08 { get; private set; }
        public uint Unknown0C { get; private set; }
        public PropType Type { get; private set; }
        /// <summary>
        /// Archive path of the model, only set for <see cref="PropType.Model"/>, <see cref="PropType.Structure"/> and <see cref="PropType.Dungeon"/>.
        /// </summary>
        public string ModelPath { get; private set; }
        public float Scale { get; private set; }
        public Quaternion Rotation { get; private set; }
        public Vector3 Position { get; private set; }

        /// <summary>
        /// Range of world cells (32 units, 1024 is the world origin) the prop covers.
        /// </summary>
        public ushort MinCellX { get; private set; }
        public ushort MinCellY { get; private set; }
        public ushort MaxCellX { get; private set; }
        public ushort MaxCellY { get; private set; }

        public uint Unknown44 { get; private set; }
        public uint Colour { get; private set; }
        public uint Unknown54 { get; private set; }
        public uint Unknown5C { get; private set; }

        private uint modelPathOffset;
        private uint unknown18Offset;

        public void Read(BinaryReader reader)
        {
            UniqueId        = reader.ReadUInt32();
            ParentUniqueId  = reader.ReadUInt32();
            Unknown08       = reader.ReadUInt32();
            Unknown0C       = reader.ReadUInt32();

            Type            = (PropType)reader.ReadUInt32();
            modelPathOffset = reader.ReadUInt32();
            unknown18Offset = reader.ReadUInt32();
            Scale           = reader.ReadSingle();
            Rotation        = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Position        = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            MinCellX        = reader.ReadUInt16();
            MinCellY        = reader.ReadUInt16();
            MaxCellX        = reader.ReadUInt16();
            MaxCellY        = reader.ReadUInt16();
            Unknown44       = reader.ReadUInt32();
            reader.ReadBytes(8);
            Colour          = reader.ReadUInt32();
            Unknown54       = reader.ReadUInt32();
            reader.ReadUInt32();
            Unknown5C       = reader.ReadUInt32();
            reader.ReadBytes(8);
        }

        /// <summary>
        /// Read the strings, their offsets are relative to the start of the chunk.
        /// </summary>
        public void ReadStrings(BinaryReader reader)
        {
            if (Type is PropType.Model or PropType.Structure or PropType.Dungeon && modelPathOffset != 0u)
                ModelPath = ReadWideString(reader, modelPathOffset);
        }

        private static string ReadWideString(BinaryReader reader, uint offset)
        {
            if (offset >= reader.BaseStream.Length)
                return null;

            reader.BaseStream.Position = offset;

            var builder = new System.Text.StringBuilder();
            while (reader.BaseStream.Position + 2 <= reader.BaseStream.Length)
            {
                char c = (char)reader.ReadUInt16();
                if (c == '\0')
                    break;
                builder.Append(c);
            }

            return builder.ToString();
        }
    }
}
