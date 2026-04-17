using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameEndUiMan : NetworkBehaviour
{
    
    public TextMeshProUGUI loss;
    public TextMeshProUGUI win;

    
    void OnEnable()
    //void Start()
    {
        if (GameManager.Instance == null) Debug.Log("null");
        
        GameManager.Instance.onGameOver += HandleGameOver;
        
    }
    void OnDisable()
    {

        GameManager.Instance.onGameOver -= HandleGameOver;
    }
    void HandleGameOver(int cleared)
    {
        /*
        if (cleared>0)
        {
            win.text = "You Won!";
            loss.text = "";
        }
        else
        {
            win.text = "";
            loss.text = "You\nlost...";
        }
        //*/

    }

    public void ReturnToMap()
    {
        if(IsServer)
        {
            NetworkManager.SceneManager.LoadScene("WorldMap", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

    void Start()
    {

        int cleared = GameManager.Instance.getPlayersCleared();
        if (cleared>0)
        {
            win.text = "You Won!";
            loss.text = "";
        }
        else
        {
            win.text = "";
            loss.text = "You\nlost...";
        }
    }
}