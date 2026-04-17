using Unity.Netcode; 
using UnityEngine; 
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;

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
            
            GameObject playerInstance = Instantiate(playerPrefab, new Vector3(-6.5f -0.75f*index, -3.75f, 0), Quaternion.identity);
            SpriteRenderer sr = playerInstance.GetComponent<SpriteRenderer>();
            if (index == 0)
            {
                if (sr != null) 
                {
                    sr.color = Color.blue;
                }
            }
            else if (index == 1)
            {
                if (sr != null) 
                {
                    sr.color = Color.red;
                }
            }
            else if (index == 2)
            {
                if (sr != null) 
                {
                    sr.color = Color.green;
                }
            }
            else if (index == 3)
            {
                if (sr != null) 
                {
                    sr.color = Color.yellow;
                }
            }
            
            NetworkObject networkObject = playerInstance.GetComponent<NetworkObject>();
            networkObject.SpawnAsPlayerObject(clientId);
            index++;
            
        } 
    } 

    
    
} 