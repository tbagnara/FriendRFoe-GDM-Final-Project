using UnityEngine;
using System.IO;
using Unity.Netcode;
using UnityEditor.Overlays;

[System.Serializable]
public class PlayerSaveData
{
    public bool lvl1Clear;
    public bool lvl2Clear;
    public float totalPlayTime;
    public string lastPlayed;
}

public class SaveLoadManager : NetworkBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    
    private PlayerSaveData playerData;
    private string savePath;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        savePath = Path.Combine(Application.persistentDataPath, "playersave.json");
        LoadPlayerData();
    }
    
    public void LoadDataForAll()
    {
        if (IsServer)
        {
            UpdateSaveDataRpc(playerData);
            Debug.Log("dataloadsent");
        }
    }

    void UpdateSaveDataRpc(PlayerSaveData playerdata)
    {
        playerData = playerdata;
        Debug.Log("dataloaded");
    }
    
    public void SavePlayerData()
    {
        playerData.lastPlayed = System.DateTime.Now.ToString();
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Player data saved!");
    }
    
    public void LoadPlayerData()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string json = File.ReadAllText(savePath);
                playerData = JsonUtility.FromJson<PlayerSaveData>(json);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Load failed: " + e.Message);
                CreateNewPlayerData();
            }
        }
        else
        {
            CreateNewPlayerData();
        }
    }
    
    void CreateNewPlayerData()
    {
        playerData = new PlayerSaveData();
        playerData.lvl1Clear = false;
        playerData.lvl1Clear = false;
        playerData.totalPlayTime = 0f;
        playerData.lastPlayed = System.DateTime.Now.ToString();
        SavePlayerData();
    }
    
    public void RecordCleared(string level)
    {
        if (level.Equals("1 - 1"))
        {
            playerData.lvl1Clear = true;
            //Debug.Log("WEMADEIT");
        }
        else if (level.Equals("1 - 2"))
        {
            playerData.lvl2Clear = true;
        }
        SavePlayerData();
    }
    public bool GetLvl1Clear() { return playerData.lvl1Clear; }
    public bool GetLvl2Clear() { return playerData.lvl2Clear; }

    
    void OnApplicationQuit()
    {
        SavePlayerData();
    }
}