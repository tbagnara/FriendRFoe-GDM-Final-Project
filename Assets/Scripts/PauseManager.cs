using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : NetworkBehaviour
{
    [SerializeField] private Slider SFXslider;
    [SerializeField] private Slider Musicslider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SFXslider.onValueChanged.AddListener((v) =>
        {
            AudioManager.Instance.sfxSource.volume = v;
        });
        Musicslider.onValueChanged.AddListener((v) =>
        {
            AudioManager.Instance.musicSource.volume = v;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnExitPauseButton()
    {
        if(IsServer)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(GameManager.Instance.getLevel(), LoadSceneMode.Single);
            
        }
        GameManager.Instance.ResetGameClears();
    }

    public void ExitLevel()
    {
        if(IsServer)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
        }
    }



}
