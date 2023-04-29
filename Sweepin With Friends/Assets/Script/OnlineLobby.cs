using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnlineLobby : MonoBehaviour
{
    public static OnlineLobby Instance { get; set; }

    public Server server;
    public Client client;

    [SerializeField] private TMP_InputField addressInput;


    private void Awake()
    {
        Instance = this;
    }




    public void OnLocalGameButton()
    {

        server.Init(8007);
        client.Init("127.0.0.1", 8007);
        Debug.Log("OnLocalGameButton");

    }

    public void OnOnlineGameButton()
    {
        Debug.Log("OnOnlineGameButton");

    }

    public void OnOnlineHostButton()
    {
        server.Init(8007);
        client.Init("127.0.0.1", 8007);
        Debug.Log("OnOnlineHostButton");

    }
    public void OnOnlineConnectButton()
    {
        Debug.Log("Game Over!");
        client.Init(addressInput.text, 8007);
        //client.Init("127.0.0.1", 8007);


    }

    public void OnOnlineBackButton()
    {
        Debug.Log("OnOnlineBAckButton");

    }

    public void OnHostBackButton()
    {
        server.Shutdown();
        client.Shutdown();

    }

}
