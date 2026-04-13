using Unity.Netcode;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    //[SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerInput;

    public override void OnNetworkSpawn()
    {
        //playerCamera.SetActive(IsOwner);
        playerInput.SetActive(true);
    }
}
