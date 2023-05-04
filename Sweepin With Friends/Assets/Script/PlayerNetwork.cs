using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.EventSystems;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1);

    private void Update()
    {
        if (!IsOwner) return;

        //Vector3 mousePosition = Input.mousePosition;


        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        float moveSpeed = 55f;

       transform.position = mousePosition;

    }
}
