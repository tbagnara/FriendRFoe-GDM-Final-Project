using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using System;
using System.Collections.Generic;
using System.Numerics;

public class MapController : NetworkBehaviour
{
    private Rigidbody2D rb;
    public int level;
    public int world;

    public TextMeshProUGUI worldLevel;
    public TextMeshProUGUI FirstClearPlayer;
    public TextMeshProUGUI BestTimePlayer;
    public TextMeshProUGUI BestTimeTime;
    public TextMeshProUGUI MostCoinsPlayer;
    public TextMeshProUGUI MostCoinsCoins;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GameManager.Instance.getLevel() == "1 - 2")
        {
            Debug.Log(GameManager.Instance.getLevel());
            rb.position = new UnityEngine.Vector2(-2.57f,-1.93f);
            world = 1;
            level = 2;
        }
        else
        {
            world = 1;
            level = 1;
        }
        worldLevel.text = world + " - " + level;

        GameManager.Instance.setLevel(SceneManager.GetActiveScene().name);
        SaveLoadManager.Instance.LoadPlayerData();
        
    }
        

    
    void Update()
    {
        Movement();
        Select();
    }

    void Movement()
    {
        if (SaveLoadManager.Instance.GetLvl1Clear())
        {
            if (Input.GetAxis("Horizontal") > 0 && level == 1)
            {
                level = 2;
                rb.linearVelocityX = 10;
                
            }
            else if (Input.GetAxis("Horizontal") < 0 && level == 2)
            {
                level = 1;
                rb.linearVelocityX = -10;
            }

            if (rb.position.x <-6.94 && rb.linearVelocityX <0 || rb.position.x >-2.65 && rb.linearVelocityX >0)
            {
                rb.linearVelocityX = 0;
            }   
        }
        if (IsServer)
        {
            UpdateWorldTextRpc(world + " - " + level);
            UIUpdate();
        }
    }

    void Select()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //SceneManager.LoadScene(""+world + " - " + level);
            NetworkManager.Singleton.SceneManager.LoadScene(""+world + " - " + level, LoadSceneMode.Single);
        }
    }

    [Rpc(SendTo.Everyone)]
    void UpdateWorldTextRpc(String lvl)
    {
        worldLevel.text = lvl;
        //Debug.Log(lvl);
    }

    public void PauseMenuPressed()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("PauseMenu", LoadSceneMode.Single);
    }

    public void UIUpdate()
    {
        
        List<HighScore> firstClear = DatabaseManager.Instance.GetFirstClear(worldLevel.text);
        if (firstClear.Count > 0)
        {
            FirstClearPlayer.text = firstClear[0].PlayerNumber;
        }
        else
        {
            FirstClearPlayer.text = "--";
        }
        
        List<HighScore> BestTime = DatabaseManager.Instance.GetTopTime(worldLevel.text);
        if (BestTime.Count > 0)
        {
            BestTimePlayer.text = BestTime[0].PlayerNumber;
            BestTimeTime.text = ""+(int) BestTime[0].CompletionTime;
        }
        else
        {
            BestTimePlayer.text = "--";
            BestTimeTime.text = "--";
        }
        
        List<HighScore> BestScore = DatabaseManager.Instance.GetTopHighScores(worldLevel.text);
        if (BestScore.Count >0)
        {
            MostCoinsPlayer.text = BestScore[0].PlayerNumber;
            MostCoinsCoins.text = ""+BestScore[0].Score;
        }
        else
        {
            MostCoinsPlayer.text = "--";
            MostCoinsCoins.text = "--";
        }
        UIUpdateRpc(FirstClearPlayer.text, BestTimePlayer.text, BestTimeTime.text, MostCoinsPlayer.text, MostCoinsCoins.text);
        
        
    }
    [Rpc(SendTo.NotServer)]
    public void UIUpdateRpc(string FCP, string BTP, string BTT, string MCP, string MCC)
    {
        FirstClearPlayer.text = FCP;
        BestTimePlayer.text = BTP;
        BestTimeTime.text = BTT;
        MostCoinsPlayer.text = MCP;
        MostCoinsCoins.text = MCC;
    }


}


