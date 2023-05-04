using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.EventSystems;

public class PlayerNetwork : NetworkBehaviour
{

    private void Update()
    {
        if(!IsOwner) return;


        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


       transform.position = mousePosition;

    }
}
