using System;
using System.Collections.Generic;
using System.Text;
using KaymakNetwork;
using System.Numerics;


enum ClientPackets
{
    CPing = 1,
    CKeyInput,
    CPlayerRotation,
    CMessage,
    
}

internal static class NetworkReceive
{
    internal static void PacketRouter()
    {
        NetworkConfig.socket.PacketId[(int)ClientPackets.CPing] = Packet_Ping;
        NetworkConfig.socket.PacketId[(int)ClientPackets.CKeyInput] = Packet_KeyInput;
        NetworkConfig.socket.PacketId[(int)ClientPackets.CPlayerRotation] = Packet_PlayerRotation;
        NetworkConfig.socket.PacketId[(int)ClientPackets.CMessage] = Packet_Message;
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
        float rotX = buffer.ReadSingle();
        float rotY = buffer.ReadSingle();
        float rotZ = buffer.ReadSingle();
        float rotW = buffer.ReadSingle();

        buffer.Dispose();

        GameManager.playerList[connectionID].rotation = new Quaternion(rotX, rotY, rotZ, rotW);
    }
    private static void Packet_Message(int connectionID, ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        string message = buffer.ReadString();

        buffer.Dispose();

        NetworkSend.SendMessage(connectionID, message);

    }
}

