using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIManager : NetworkBehaviour
{
    //public TextMeshProUGUI scoreText1;
    //public TextMeshProUGUI scoreText2;
    //public TextMeshProUGUI scoreText3;
    //public TextMeshProUGUI scoreText4;
    [SerializeField] public List<TextMeshProUGUI> scoreTexts;
    public TextMeshProUGUI healthText;
    
    void OnEnable()
    //void Start()
    {
        if (GameManager.Instance == null) Debug.Log("null");
        GameManager.Instance.onScore1Changed += UpdateScore1;
        GameManager.Instance.onScore2Changed += UpdateScore2;
        GameManager.Instance.onScore3Changed += UpdateScore3;
        GameManager.Instance.onScore4Changed += UpdateScore4;
        GameManager.Instance.onHealthChanged += UpdateHealth;
        GameManager.Instance.onGameOver += HandleGameOver;
        for (int i = 0; i <NetworkManager.Singleton.ConnectedClientsList.Count; i++)
        {
            scoreTexts[i].text = "Player " + (i+1) + "score:";
        }
    }
    void OnDisable()
    {
        GameManager.Instance.onScore1Changed -= UpdateScore1;
        GameManager.Instance.onScore1Changed -= UpdateScore2;
        GameManager.Instance.onScore1Changed -= UpdateScore3;
        GameManager.Instance.onScore1Changed -= UpdateScore4;
        GameManager.Instance.onHealthChanged -= UpdateHealth;
        GameManager.Instance.onGameOver -= HandleGameOver;
    }
    void UpdateScore1(int score)
    {
        scoreTexts[0].text = "Player 1 score: " + score;
    }
    void UpdateScore2(int score)
    {
        scoreTexts[1].text = "Player 2 score: " + score;
    }
    void UpdateScore3(int score)
    {
        scoreTexts[2].text = "Player 3 score: " + score;
    }
    void UpdateScore4(int score)
    {
        scoreTexts[3].text = "Player 4 score: " + score;
    }
    void UpdateHealth(int newHealth)
    {
        healthText.text = "Health: " + newHealth;
    }
    void HandleGameOver()
    {
        NetworkManager.SceneManager.LoadScene("Highcores", LoadSceneMode.Single);
    }

    

}