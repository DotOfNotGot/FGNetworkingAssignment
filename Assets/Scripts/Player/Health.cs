using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    public NetworkVariable<int> currentHealth = new NetworkVariable<int>();


    public override void OnNetworkSpawn()
    {
        if(!IsServer) return;
        currentHealth.Value = 100;
    }


    public void TakeDamage(int damage){
        damage = damage<0? damage:-damage;
        currentHealth.Value += damage;

        if (currentHealth.Value <= 0)
        {
            ulong playerID = NetworkObject.OwnerClientId;
            NetworkObject.Despawn();
            
            NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(NetworkManager.NetworkConfig.PlayerPrefab.GetComponent<NetworkObject>(), ownerClientId: playerID,
                destroyWithScene: true, isPlayerObject: true);
        }
    }

    public void GainHealth(int value)
    {
        value = value < 0 ? -value : value;
        currentHealth.Value += value;

        if (currentHealth.Value > 100)
        {
            currentHealth.Value = 100;
        }
    }
}
