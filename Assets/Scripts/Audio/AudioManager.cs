using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;  // Para la m�sica de fondo
    public AudioSource sfxSource;   // Para los efectos de sonido

    [Header("Audio Clips")]
    public AudioClip backgroundMenuMusic; // M�sica de fondo inicial
    public AudioClip backgroundGameMusic; // M�sica de fondo inicial
    public AudioClip backgroundEpicMusic; // M�sica de fondo inicial

    [Header("Audios SFX")]
    public AudioClip[] auchList;
    public AudioClip[] wihList;
    public AudioClip[] popList;
    public AudioClip deathAudio;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    // Reproducir m�sica de fondo
    public void PlayMusic(AudioClip clip)
    {
        if (clip != null && musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.loop = true; // La música suele repetirse
            musicSource.Play();
        }
    }

    // Reproducir un efecto de sonido
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.pitch = Random.Range(.9f, 1.1f);
            sfxSource.PlayOneShot(clip); // No interrumpe otros sonidos
        }
    }

    public void PlayPop()
    {
        PlaySoundList(popList);
    }

    public void PlayAuch()
    {
        PlaySoundList(auchList);
    }

    public void PlayWih()
    {
        PlaySoundList(wihList);
    }

    public void PlaySoundList(AudioClip[] list)
    {
        if (list.Length == 0)
            return;

        int n = Random.Range(0, list.Length);

        AudioClip clip = list[n];
        PlaySFX(clip);
    }

    // Cambiar el volumen de la m�sica
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume); // Entre 0 y 1
    }

    // Cambiar el volumen de los efectos de sonido
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume); // Entre 0 y 1
    }

    public void PlayMenu()
    {
        PlayMusic(backgroundMenuMusic);
    }
    
    public void PlayGame()
    {
        PlayMusic(backgroundGameMusic);
    }
    
    public void PlayEpic()
    {
        PlayMusic(backgroundEpicMusic);
    }

    public void PlayDeathMusic()
    {
        PlayMusic(deathAudio);
    }
}
