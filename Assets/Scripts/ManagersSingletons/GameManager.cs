using System;
using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<int> onScoreChanged;
    public event Action<int> onScore1Changed;
    public event Action<int> onScore2Changed;
    public event Action<int> onScore3Changed;
    public event Action<int> onScore4Changed;

    public event Action<int> onHealthChanged;
    public event Action onGameOver;
    //private int score = 0;
    private int health = 10;
    private float completionTime = 0f;

    public int p1Score = 0;
    public int p2Score = 0;
    public int p3Score= 0;
    public int p4Score = 0;



    
    


    private int playersCleared = 0;
    private int playersDied = 0;


    void Awake()
    {
        if(Instance!=null && Instance !=this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void addCleared()
    {
        playersCleared++;
    }

    public void addDied()
    {
        playersDied++;
    }

    
    void Update()
    {   
        
        if ((playersCleared+playersDied) >= NetworkManager.Singleton.ConnectedClientsIds.Count) {
            if (playersCleared>0)
            {
                //onGameOver?.Invoke();
                NetworkManager.SceneManager.LoadScene("WorldMap", UnityEngine.SceneManagement.LoadSceneMode.Single);

            }
            else
            {
                NetworkManager.SceneManager.LoadScene("WorldMap", UnityEngine.SceneManagement.LoadSceneMode.Single);
            }
            playersCleared = 0;
            playersDied = 0;
            //ResetGameStats();
        }
        
        /*
        if (FindFirstObjectByType<PlayerController>() == null)
        {
            NetworkManager.SceneManager.LoadScene("WorldMap", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }*/

    }
    
    
    public void AddScore(int player, int s1, int s2, int s3, int s4)
    {
        p1Score = s1;
        p2Score = s2;
        p3Score = s3;
        p4Score = s4;
        if (player == 1)
        {   

            onScore1Changed?.Invoke(p1Score);
        }
        else if (player == 2)
        {   

            onScore2Changed?.Invoke(p2Score);
        }
        else if (player == 3)
        {   
            onScore3Changed?.Invoke(p3Score);
        }
        else if (player == 4)
        {   
            onScore4Changed?.Invoke(p4Score);
        }
        Debug.Log(p1Score +" - "+p2Score);
        //onScoreChanged?.Invoke(10);
    }



    public void AddScoreServ(int player)
    {
        if (player == 1)
        {   
            p1Score += 10;
            Debug.Log("add 10 to score" + p1Score);    
            //onScore1Changed?.Invoke(p1Score.Value);
        }
        else if (player == 2)
        {   
            p2Score += 10;  
            //onScore2Changed?.Invoke(p2Score.Value);
        }
        else if (player == 3)
        {   

            p3Score += 10; 
            //onScore3Changed?.Invoke(p3Score.Value);
        }
        else if (player == 4)
        {   


            p4Score += 10;

            //onScore4Changed?.Invoke(p4Score.Value);
        }
        //onScoreChanged?.Invoke(10);
        //AddScoreRpc(player);
    }




    public float getTime()
    {
        return completionTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        onHealthChanged?.Invoke(health);
        if(health <= 0)
        {
            completionTime = Time.timeSinceLevelLoad;
            //onGameOver?.Invoke();
        }
    }
    void Start()
    {
        p1Score = 0;
        p2Score = 0;
        p3Score = 0;
        p4Score = 0;
        health = 10;
        
    }
    
}
