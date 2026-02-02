using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip menuMusic;
    public AudioClip gameMusic;

    private AudioSource audioSource;
    private string currentSceneName;

    private void Awake()
    {
        // Singleton - tylko jedna instancja AudioManagera
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Nie niszcz przy zmianie sceny
            audioSource = GetComponent<AudioSource>();
            
            // Jeśli nie ma AudioSource, dodaj
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            
            audioSource.loop = true; // Muzyka gra w pętli
        }
        else
        {
            Destroy(gameObject); // Zniszcz duplikat
            return;
        }
    }

    private void Start()
    {
        // Zarejestruj callback na zmianę sceny
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        // Odtwórz muzykę dla aktualnej sceny
        PlayMusicForCurrentScene();
    }

    private void OnDestroy()
    {
        // Usuń callback żeby uniknąć memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Gdy scena się zmieni, odtwórz odpowiednią muzykę
        PlayMusicForCurrentScene();
    }

    private void PlayMusicForCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // Sprawdź czy to ta sama scena - nie zmieniaj muzyki
        if (sceneName == currentSceneName)
            return;

        currentSceneName = sceneName;

        // Wybierz muzykę w zależności od sceny
        if (sceneName == "Menu")
        {
            PlayMusic(menuMusic);
        }
        else // Wszystkie inne sceny (gra)
        {
            PlayMusic(gameMusic);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioManager: Brak przypisanego AudioClip!");
            return;
        }

        // Jeśli ta sama muzyka już gra, nie restartuj
        if (audioSource.clip == clip && audioSource.isPlaying)
            return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    // Opcjonalne metody dodatkowe
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
