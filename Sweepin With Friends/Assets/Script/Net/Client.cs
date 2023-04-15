using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;
using Unity.Collections;
using System;
//using UnityEditorInternal.Profiling;

public class Client : MonoBehaviour
{

    public static Client Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }

    public NetworkDriver driver;
    private NetworkConnection connection;

    private bool isActive = false;


    public Action connectionDropped;

    public void Init(string ip, ushort port)
    {

        driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.Parse(ip, port) ;

        connection = driver.Connect(endpoint);

        isActive = true;

        RegisterToEvent();

    }

    public void Shutdown()
    {
        if (isActive)
        {
            UnregisterToEvent();

            driver.Dispose();
            
            isActive = false;
            connection = default(NetworkConnection);

        }
    }

    public void OnDestroy()
    {
        Shutdown();
    }

    public void Update()
    {
        if (!isActive)
        {
            return;
        }


        driver.ScheduleUpdate().Complete();

        CheckAlive();

        //CleanupConnections();
        //AcceptNewConnections();
        UpdateMessagePump();

    }

    private void CheckAlive()
    {
        if(!connection.IsCreated && isActive)
        {
            connectionDropped?.Invoke();
            Shutdown();
        }
    }

    private void UpdateMessagePump()
    {
        DataStreamReader stream;

            NetworkEvent.Type cmd;
            while ((cmd = connection.PopEvent(driver, out stream)) != NetworkEvent.Type.Empty)
            {

                if (cmd == NetworkEvent.Type.Connect)
                {

                //SendToServer(new NetWelcome());

                }
                else if (cmd == NetworkEvent.Type.Data)
            {
    
                //NetUtility.OnData(stream, default(NetworkConnection));

            }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                   connection = default(NetworkConnection) ;
                connectionDropped?.Invoke();
                Shutdown();

                }

            }
        
    }


    public void SendToServer(NetMessage msg)
    {
        DataStreamWriter writer;
        driver.BeginSend(connection, out writer);
        msg.Serialize(ref writer);
        driver.EndSend(writer);
    }

    private void RegisterToEvent()
    {
        //NetUtility.C_KEEP_ALIVE += OnKeepAlive;

    }

    private void UnregisterToEvent() {

        //NetUtility.C_KEEP_ALIVE -= OnKeepAlive;

    }

    private void OnKeepAlive(NetMessage nm)
    {
        SendToServer(nm);
    }

}
