using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    //SerializeFiel - se usa quando o atribute deve ser privado, mas aparacer no editor
    [SerializeField] Slider VolumeSlider;

    [Header("Efeitos Sonoros")]
    public AudioClip jumpSound;
    public AudioClip collectSound;

    private AudioSource audioSource;
    private AudioClip moedaSound;

    void Awake()
    {
        // Garante que só exista um AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // não destruir entre cenas
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        //volume do jogo será igual ao do controle deslizante
        AudioListener.volume = VolumeSlider.value;
        Save();
    }

    private void Load()
    {
        //garante que o valor do controle deslizante seja igual ao armazenado
        VolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        //armazena o valor do volume
        PlayerPrefs.SetFloat("musicVolume", VolumeSlider.value);
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayAudioMoeda()
    {
        audioSource.PlayOneShot(moedaSound);
    }


}
