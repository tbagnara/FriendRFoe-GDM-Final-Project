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
    public TextMeshProUGUI P2ready;
    public TextMeshProUGUI P3ready;
    public TextMeshProUGUI P4ready;

    public TextMeshProUGUI[] players = new TextMeshProUGUI[4];
    

    void Start()
    {
        
        players[0] = P1ready;
        players[1] = P2ready;
        players[2] = P3ready;
        players[3] = P4ready;
    }

    void Update()
    {
        
        if (players[0].text.Equals("Yes"))
        {
            //SceneManager.LoadScene("WorldMap");
            NetworkManager.SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
        }
    }

    public void PlayersReady(int player)
    {
        player -= 1;
        if (!players[player].text.Equals("Yes"))
        {
            players[player].text = "Yes";
        }
        else
        {
            players[player].text = "No";
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
