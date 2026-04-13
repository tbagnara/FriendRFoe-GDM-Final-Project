using Unity.Netcode; 
using UnityEngine; 
using System.Collections.Generic; 
 
public class PlayerSpawner : NetworkBehaviour 
{ 
    [SerializeField] private List<NetworkObject> playerSlots; 
    [SerializeField] private GameObject playerPrefab;
    // Drag your pre-placed disabled player objects here in the Inspector 
 
    public override void OnNetworkSpawn() 
    { 
        if (!IsServer) return; 
        AssignSlots(); 
    } 
 
    private void AssignSlots() 
    { 
        int index = 0; 
 
        foreach (var clientId in NetworkManager.Singleton.ConnectedClientsIds) 
        { 
            if (index >= playerSlots.Count) 
            { 
                Debug.LogWarning("More players than available slots!"); 
                break; 
            } 
            
            //NetworkManager.Singleton.ConnectedClient
            
            GameObject playerInstance = Instantiate(playerPrefab, new Vector3(-6.5f -0.5f*index, -4.5f, 0), Quaternion.identity);
            NetworkObject networkObject = playerInstance.GetComponent<NetworkObject>();
            networkObject.SpawnAsPlayerObject(clientId);
            //*/
            

            /*

 
            NetworkObject slot = playerSlots[index]; 
 
            // Give this slot ownership to the connected client 
            slot.SpawnWithOwnership(clientId); 
 
            // Activate it across all clients 
            slot.gameObject.SetActive(true); 
 
            index++; 
            */
        } 
    } 
} 