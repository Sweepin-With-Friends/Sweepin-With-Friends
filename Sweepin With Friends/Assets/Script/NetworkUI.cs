using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{

    [SerializeField] private Button serverBtn;

    [SerializeField] private Button clientBtn;
    [SerializeField] private Button hostBtn;


    private void Awake()
    {
        serverBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });


        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });


        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
    }


}
