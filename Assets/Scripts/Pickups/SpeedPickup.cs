using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpeedPickup : NetworkBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(IsServer){
            PlayerController playerController = other.GetComponent<PlayerController>();
            if(!playerController || playerController.currentMovementSpeed.Value > playerController.DefaultMovementSpeed) return;

            playerController.SetMovementSpeed(10, 5f);

            NetworkObject networkObject = gameObject.GetComponent<NetworkObject>();
            networkObject.Despawn();
        }
    }    
}
