//using Mono.Cecil.Cil;
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

public class NetMessage 
{
   
    public OpCode Code { get; set; }



public virtual void Serialize(ref DataStreamWriter writer)
{
    writer.WriteByte((byte)Code);
}

public virtual void Deserialize(DataStreamReader reader)
{

}

public virtual void ReceivedOnClient()
{

}

public virtual void ReceivedOnServer(NetworkConnection cnn)
{

}

}
