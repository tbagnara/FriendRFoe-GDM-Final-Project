using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip coinSound;
    public AudioClip jumpSound;
    public AudioClip damageSound;
    public AudioClip backgroundMusic;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /*
    void Start()
    {
        PlayMusic(backgroundMusic);
    }
    */

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    //void OnEnable()
    void Start()
    {
        GameManager.Instance.onScoreChanged += playCoinSound;
        GameManager.Instance.onHealthChanged += playDamageSound;
        PlayMusic(backgroundMusic);
    }
    void OnDisable()
    {
        GameManager.Instance.onScoreChanged -= playCoinSound;
        GameManager.Instance.onHealthChanged -= playDamageSound;
    }
    public void playCoinSound(int newscore)
    {
        PlaySoundEffect(coinSound);
    }

    public void playDamageSound(int damage)
    {
        PlaySoundEffect(damageSound);
    }
    

}
