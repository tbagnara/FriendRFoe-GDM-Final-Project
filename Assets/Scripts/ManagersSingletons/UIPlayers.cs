using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class UIPlayers : NetworkBehaviour
{
    public TextMeshProUGUI P1ready;

    void Update()
    {
        
        if (P1ready.text.Equals("Yes"))
        {
            //SceneManager.LoadScene("WorldMap");
            NetworkManager.SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
        }
    }

    public void PlayersReady(int player)
    {   
        if (IsServer)
        {
            P1ready.text = "Yes";
            NetworkManager.Singleton.SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
        }
        

    }
    
    /*
    public void Player1Ready()
    {
        if (P1ready.text.Equals("Ready?") || P1ready.text.Equals("No"))
        {
            P1ready.text = "Yes";
        }
        else
        {
            P1ready.text = "No";
        }
    }
    public void Player2Ready()
    {
        if (P2ready.text.Equals("Ready?") || P2ready.text.Equals("No"))
        {
            P2ready.text = "Yes";
        }
        else
        {
            P2ready.text = "No";
        }
    }
    public void Player3Ready()
    {
        if (P3ready.text.Equals("Ready?") || P3ready.text.Equals("No"))
        {
            P3ready.text = "Yes";
        }
        else
        {
            P3ready.text = "No";
        }
    }
    public void Player4Ready()
    {
        if (P3ready.text.Equals("Ready?") || P3ready.text.Equals("No"))
        {
            P3ready.text.Equals("Yes");
        }
        else
        {
            P3ready.text.Equals("No");
        }
    }
    */
}
