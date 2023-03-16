using System;
using KaymakNetwork;
using System.Numerics;


enum ServerPackets
{
    SWelcomeMsg = 1,
    SInstantiatePlayer,
    SPlayerMove,
    SPlayerRotation,
    SMessage,
}

internal static class NetworkSend
{
    public static void WelcomeMsg(int connectionID, string msg)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SWelcomeMsg);
        buffer.WriteInt32(connectionID);
        buffer.WriteString(msg);
        NetworkConfig.socket.SendDataTo(connectionID, buffer.Data, buffer.Head);

        buffer.Dispose();
    }

    private static ByteBuffer PlayerData(int connectionID, Player player)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SInstantiatePlayer);
        buffer.WriteInt32(connectionID);

        return buffer;
    }
    public static void InstantiateNetworkPlayer(int connectionID, Player player)
    {
        //TODO: Free Up Slots used by players who've Disconnected 
        for (int i = 1; i <= GameManager.playerList.Count; i++)
        {
            if(GameManager.playerList[i] != null)           
                if(GameManager.playerList[i].inGame)
                    if(i != connectionID)                    
                        NetworkConfig.socket.SendDataTo(connectionID, PlayerData(i, player).Data, PlayerData(i, player).Head);                              
        }
        NetworkConfig.socket.SendDataToAll(PlayerData(connectionID, player).Data, PlayerData(connectionID, player).Head);
    }

    internal static void SendPlayerMove(int connectionID, float x, float y, float z)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SPlayerMove);
        buffer.WriteInt32(connectionID);

        buffer.WriteSingle(x);
        buffer.WriteSingle(y);
        buffer.WriteSingle(z);

        //Console.WriteLine("X: " + x + " Y: " + y + " Z: " + z);
        NetworkConfig.socket.SendDataToAll(buffer.Data, buffer.Head);

        buffer.Dispose();
    }
    public static void SendPlayerRotation(int connectionID, Quaternion rotation)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SPlayerRotation);
        buffer.WriteInt32(connectionID);
        buffer.WriteSingle(rotation.X);
        buffer.WriteSingle(rotation.Y);
        buffer.WriteSingle(rotation.Z);
        buffer.WriteSingle(rotation.W);        

        if (!GameManager.playerList[connectionID].inGame) return;

        NetworkConfig.socket.SendDataToAllBut(connectionID, buffer.Data, buffer.Head);
        buffer.Dispose();

        SendPlayerRotation(connectionID, rotation);
    }

    public static void SendMessage(int connectionID, string message)
    {
        ByteBuffer buffer = new ByteBuffer(4);
        buffer.WriteInt32((int)ServerPackets.SMessage);
        buffer.WriteInt32(connectionID);

        string newMessage = "Player " + connectionID + ": " + message + "\n";

        buffer.WriteString(newMessage);
        Console.WriteLine(newMessage);
        NetworkConfig.socket.SendDataToAll(buffer.Data, buffer.Head);
        buffer.Dispose();
    }
}

