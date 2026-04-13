using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private int requiredPlayers = 2;
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
    }

    
    void Update()
    {
        if(!NetworkManager.Singleton.IsHost) return;
        if(NetworkManager.Singleton.ConnectedClients.Count >= requiredPlayers)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
        }
    }
}
