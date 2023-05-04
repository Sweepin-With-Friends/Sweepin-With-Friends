using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.EventSystems;

public class PlayerNetwork : NetworkBehaviour
{

    private void Update()
    {
        if (!IsOwner) return;

        Vector3 moveDir = new Vector3(0, 0, 0);


        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        float moveSpeed = 55f;

        transform.position += worldPosition * moveSpeed * Time.deltaTime;

    }
}
