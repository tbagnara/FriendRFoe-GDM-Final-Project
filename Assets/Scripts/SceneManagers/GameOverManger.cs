using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class GameOverManger : NetworkBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TMP_InputField playerNameInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int finalScore = GameManager.Instance.p1Score;
        finalScoreText.text = "Score: " + finalScore;
    }

    public void OnSubmitScore()
    {
        string playerName = playerNameInput.text;
        
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Anonymous";
        }
        
        int finalScore = GameManager.Instance.p1Score;
        float completionTime = GameManager.Instance.getTime();
        
        DatabaseManager.Instance.SaveHighScore(playerName, finalScore, completionTime);
        
        NetworkManager.SceneManager.LoadScene("HighScores", LoadSceneMode.Single);
    }        
    public void LoadGameScene()
    {
        //CoinPoolManager.Instance.ResetAllCoins();
        OnSubmitScore();
        NetworkManager.SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
    }

}