using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AmmoPickup : NetworkBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(IsServer){
            FiringAction firingAction = other.GetComponent<FiringAction>();
            if(!firingAction || firingAction.currentAmmo.Value == 10) return;
            firingAction.GainAmmo(5);

            NetworkObject networkObject = gameObject.GetComponent<NetworkObject>();
            networkObject.Despawn();
        }
    }
}
