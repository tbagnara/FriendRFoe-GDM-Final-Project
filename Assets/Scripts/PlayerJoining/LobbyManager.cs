using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LobbyManager : NetworkBehaviour
{

    [SerializeField] private List<GameObject> slots;
    void Start()
    {
        #if UNITY_EDITOR
        if (ParrelSync.ClonesManager.IsClone())
        {
            NetworkManager.Singleton.StartClient();
        }
        else
        {
            NetworkManager.Singleton.StartHost();
            #endif
        }
        NetworkManager.Singleton.OnClientConnectedCallback += OnPlayerConnect;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnPlayerConnect;
    }
    void OnDisable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= OnPlayerConnect;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnPlayerConnect;
    }
    
    void Update()
    {
        

        /*
        if(!NetworkManager.Singleton.IsHost) return;
        if(NetworkManager.Singleton.ConnectedClients.Count >= requiredPlayers)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
        }
        */
    }

    void OnPlayerConnect(ulong clientId)
    {
        if (NetworkManager.Singleton.ConnectedClients.Count == 1)
        {
            slots[1].SetActive(false);
            slots[2].SetActive(false);
            slots[3].SetActive(false);
        }
        else if (NetworkManager.Singleton.ConnectedClients.Count == 2)
        {
            slots[1].SetActive(true);
            slots[2].SetActive(false);
            slots[3].SetActive(false);
        } 
        else if (NetworkManager.Singleton.ConnectedClients.Count == 3)
        {
            slots[1].SetActive(true);
            slots[2].SetActive(true);
            slots[3].SetActive(false);
        }
        else if (NetworkManager.Singleton.ConnectedClients.Count == 4)
        {
            slots[1].SetActive(true);
            slots[2].SetActive(true);
            slots[3].SetActive(true);
        }
    }

    [Rpc(SendTo.Everyone)]
    void spawnPlayerRpc()
    {
        if (NetworkManager.Singleton.ConnectedClients.Count == 2)
        {
            slots[1].SetActive(true);
        } 
        else if (NetworkManager.Singleton.ConnectedClients.Count == 3)
        {
            slots[2].SetActive(true);
        }
        else if (NetworkManager.Singleton.ConnectedClients.Count == 4)
        {
            slots[3].SetActive(true);
        }
    }
}
