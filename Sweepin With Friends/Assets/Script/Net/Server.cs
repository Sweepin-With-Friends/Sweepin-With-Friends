using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;
using Unity.Collections;
using System;
//using UnityEditorInternal.Profiling;

public class Server : MonoBehaviour
{

    public static Server Instance { get;set; }
    private void Awake()
    {
        Instance = this; 
    }

    public NetworkDriver driver;
    private NativeList<NetworkConnection> connections;

    private bool isActive = false;
    private const float keepAliveTickRate = 20.0f;
    private float lastKeepAlive;

    public Action connectionDropped;

    public void Init(ushort port)
    {

        driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.AnyIpv4;
        endpoint.Port = port;

        if (driver.Bind(endpoint) != 0)
        {

            return;

        }
        else
        {
            driver.Listen();
        }

        connections = new NativeList<NetworkConnection>(2,Allocator.Persistent);
        isActive= true;

    }

    public void Shutdown()
    {
        if (isActive)
        {
            driver.Dispose();
            connections.Dispose();
            isActive = false;

        }  
    }

    public void OnDestroy()
    {
        Shutdown();
    }

    public void Update()
    {
        if(!isActive)
        {
            return;
        }

        //KeepAlive();

        driver.ScheduleUpdate().Complete();
        CleanupConnections();
        AcceptNewConnections();
        UpdateMessagePump();

    }

    private void CleanupConnections()
    {
        for(int i = 0; i < connections.Length; i++)
        {
            if (!connections[i].IsCreated)
            {
                connections.RemoveAtSwapBack(i);
                --i;
            }
        }
    }

    private void AcceptNewConnections()
    {
        NetworkConnection c;
        while((c = driver.Accept()) != default(NetworkConnection))
        {
            connections.Add(c);
        }
    }

    private void UpdateMessagePump()
    {
        DataStreamReader stream;
        for(int i = 0; i < connections.Length;i++)
        {
            NetworkEvent.Type cmd;
            while((cmd = driver.PopEventForConnection(connections[i],out stream)) != NetworkEvent.Type.Empty) {
            
                if(cmd== NetworkEvent.Type.Data) {
                
                    //NetUtility.OnData(stream, connections[i],this);

                }
                else if(cmd == NetworkEvent.Type.Disconnect){
                    connections[i] = default(NetworkConnection);
                    connectionDropped?.Invoke();
                    Shutdown();

                }
            
            }
        }
    }

    public void Broadcast(NetMessage msg)
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].IsCreated)
            {
                //SendtoCLient(connections[i],msg);
            }
        }
    }

    public void SendToClient(NetworkConnection connection, NetMessage msg)
    {
        DataStreamWriter writer;
        driver.BeginSend(connection, out writer);
        msg.Serialize(ref writer);
        driver.EndSend(writer);
    }


}


