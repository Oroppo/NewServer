using System;
using System.Collections.Generic;
using System.Text;
using KaymakNetwork;


enum ClientPackets
{
    CPing = 1,
    CKeyInput,
    CPlayerRotation,
    
}
internal static class NetworkReceive
{
    internal static void PacketRouter()
    {
        NetworkConfig.socket.PacketId[(int)ClientPackets.CPing] = Packet_Ping;
        NetworkConfig.socket.PacketId[(int)ClientPackets.CKeyInput] = Packet_KeyInput;
        NetworkConfig.socket.PacketId[(int)ClientPackets.CPlayerRotation] = Packet_PlayerRotation;
    }

    private static void Packet_Ping(int connectionID, ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        string msg = buffer.ReadString();

        Console.WriteLine(msg);
        GameManager.CreatePlayer(connectionID);
        buffer.Dispose();
    }
    private static void Packet_KeyInput(int connectionID, ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        byte key = buffer.ReadByte();

        buffer.Dispose();
        InputManager.TryToMove(connectionID, (InputManager.Keys)key);
    }
    private static void Packet_PlayerRotation(int connectionID, ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        float rotation = buffer.ReadSingle();

        buffer.Dispose();

        GameManager.playerList[connectionID].rotation = rotation;
    }
}

