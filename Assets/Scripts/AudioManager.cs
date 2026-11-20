
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Background Music")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip coinCollectSFX;
    [SerializeField] private AudioClip obstacleHitSFX;
    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip gameOverSFX;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        UpdateMusicForScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateMusicForScene(scene.name);
    }

    private void UpdateMusicForScene(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            PlayMusic(mainMenuMusic);
        }
        else if (sceneName == "GamePlay")
        {
            PlayMusic(gameplayMusic);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayCoinCollect()
    {
        PlaySFX(coinCollectSFX);
    }

    public void PlayObstacleHit()
    {
        PlaySFX(obstacleHitSFX);
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSFX);
    }

    public void PlayGameOver()
    {
        PlaySFX(gameOverSFX);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Instance = null;
        }
    }
}
