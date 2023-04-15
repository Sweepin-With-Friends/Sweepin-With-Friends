using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineLobby : MonoBehaviour
{ 
    public static OnlineLobby Instance { get; set; }

    
    private void Awake()
    {
        Instance = this;
    }



    
    public void OnLocalGameButton()
    {
        Debug.Log("OnLocalGameButton");
        
    }

    public void OnOnlineGameButton()
    {
        Debug.Log("OnOnlineGameButton");

    }

    public void OnOnlineHostButton()
    {
        Debug.Log("OnOnlineHostButton");

    }
    public void OnOnlineConnectButton()
    {
        Debug.Log("OnOnlineConnectButton");

    }

    public void OnOnlineBackButton()
    {
        Debug.Log("OnOnlineBAckButton");

    }


}
