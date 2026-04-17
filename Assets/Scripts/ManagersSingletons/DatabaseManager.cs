using UnityEngine;
using SQLite;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Events;

public class HighScore
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string PlayerLevel { get; set; }
    
    public string PlayerNumber { get; set; }
    public int Score { get; set; }
    public float CompletionTime { get; set; }
}

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }
    
    private string dbPath;
    private SQLiteConnection dbConnection;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        SetDatabasePath();
        InitializeDatabase();
        
    }
    
    void SetDatabasePath()
    {
        dbPath = Path.Combine(Application.persistentDataPath, "gamedata.db");
    }
    
    void InitializeDatabase()
    {
        dbConnection = new SQLiteConnection(dbPath);
        CreateLevelScoresTable();
    }
    
    void CreateLevelScoresTable()
    {
        dbConnection.CreateTable<HighScore>();
        Debug.Log("High Scores table created at: " + dbPath);
    }
    public void SaveHighScore(string playername, int score, float time)
    {
        return;
    }
    public void SaveLevelScore(string level, string playerNumber, int score, float completionTime)
    {
        HighScore newScore = new HighScore
        {
            PlayerLevel = level,
            PlayerNumber = playerNumber,
            Score = score,
            CompletionTime = completionTime
        };
        
        dbConnection.Insert(newScore);
        Debug.Log("High score saved: " + level + "-" + playerNumber + " - " + score);
    }

    public List<HighScore> GetTopHighScores(string level)
    {
        List<HighScore> topScores = dbConnection.Table<HighScore>()
            .OrderByDescending(score => score.Score)
            .Take(1)
            .Where(x => x.PlayerLevel.Equals(level))
            .ToList();
        
        return topScores;
    }
    public List<HighScore> GetTopTime(string level)
    {
        
        List<HighScore> topScores = dbConnection.Table<HighScore>()
            .OrderBy(time => time.CompletionTime)
            .Take(1)
            .Where(x => x.PlayerLevel.Equals(level))
            .ToList();
        
        return topScores;
    }

    public List<HighScore> GetFirstClear(string level)
    {
        List<HighScore> topScores = dbConnection.Table<HighScore>()
            .OrderBy(id => id.Id)
            .Take(1)
            .Where(x => x.PlayerLevel.Equals(level))
            .ToList();
        
        return topScores;
    }

}