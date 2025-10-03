using UnityEngine;


[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set; }
    private AudioSource audioSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSonido(AudioClip clip)
    {        
        audioSource.PlayOneShot(clip);
    }
}
