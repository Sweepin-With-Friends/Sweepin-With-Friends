//using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;


public enum OpCode
{
    KEEP_ALIVE = 1,
    WELCOME = 2,
    START_GAME = 3,
    UPDATE = 4,
    REMATCH = 5
}
public class NetUtility
{

    public static Action<NetMessage> C_KEEP_ALIVE;
    public static Action<NetMessage> C_WELCOME;
    public static Action<NetMessage> C_START_GAME;
    public static Action<NetMessage> C_UPDATE;
    public static Action<NetMessage> C_REMATCH;
    public static Action<NetMessage, NetworkConnection> S_KEEP_ALIVE;
    public static Action<NetMessage, NetworkConnection> S_WELCOME;
    public static Action<NetMessage, NetworkConnection> S_START_GAME;
    public static Action<NetMessage, NetworkConnection> S_MAKE_MOVE;
    public static Action<NetMessage, NetworkConnection> S_REMATCH;

    public static void OnData(DataStreamReader stream, NetworkConnection cnn, Server server = null)
    {
        
        NetMessage msg = null;
        var opCode =(OpCode)stream.ReadByte();
        switch(opCode)
        {
            case OpCode.KEEP_ALIVE: msg = new NetKeepAlive(stream); break;
            case OpCode.WELCOME:msg = new NetWelcome(stream); break;
            case OpCode.START_GAME:msg = new NetStartGame(stream); break;
            //case OpCode.UPDATE: msg = new NetMakeMove(stream); break;
            //case OpCode.REMATCH: msg = new NetRematch(stream);break;
            default:
                
                break;
        }

        if (server != null)
            msg.ReceivedOnServer(cnn);
        else
            msg.ReceivedOnClient();
        
    }



}
