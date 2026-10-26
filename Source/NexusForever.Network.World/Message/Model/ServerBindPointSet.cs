using NexusForever.Network.Message;

namespace NexusForever.Network.World.Message.Model
{
    /// <summary>
    /// Sets the player's current bind point.
    /// </summary>
    /// <remarks>
    /// Client handler 0x1403CBB10 stores the id as the current bind point (read by GameLib.HasBindPoint, Unit:IsCurrentBindPoint
    /// and the GoToBind game command) and refreshes the UI. The same field is sent at login in <see cref="ServerPlayerCreate.BindPointId"/>.
    /// </remarks>
    [Message(GameMessageOpcode.ServerBindPointSet)]
    public class ServerBindPointSet : IWritable
    {
        public ushort BindPointId { get; set; }

        public void Write(GamePacketWriter writer)
        {
            writer.Write(BindPointId);
        }
    }
}
