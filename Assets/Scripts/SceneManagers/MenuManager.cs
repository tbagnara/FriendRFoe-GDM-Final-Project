
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class MenuManager : NetworkBehaviour
{
    [SerializeField] private string scene = "GameScene";

    public void LoadGameScene()
    {
        NetworkManager.SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

}
