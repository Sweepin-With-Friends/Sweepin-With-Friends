using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;
//using Mono.Cecil.Cil;
using System.Runtime.Serialization;

public class NetKeepAlive : NetMessage
{
    // Start is called before the first frame update
    public NetKeepAlive()
    {
        Code = OpCode.KEEP_ALIVE;
    }

    // Update is called once per frame
    public NetKeepAlive(DataStreamReader reader)
    {
        Code = OpCode.KEEP_ALIVE;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }

    public override void Deserialize(DataStreamReader reader)
    {

    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_KEEP_ALIVE?.Invoke(this);
    }

    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_KEEP_ALIVE?.Invoke(this, cnn);
    }

}
