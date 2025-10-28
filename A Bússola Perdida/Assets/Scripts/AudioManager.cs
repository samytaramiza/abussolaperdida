using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;

    [Header("Sliders de Volume")]
    //SerializeFiel - se usa quando o atribute deve ser privado, mas aparacer no editor
    [SerializeField] Slider sliderMusica;
    [SerializeField] Slider sliderEfeitos;

    [Header("Efeitos Sonoros")]
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip potionSound;

    [Header("Fontes de Áudio")]
    public AudioSource musicaSource;
    public AudioSource efeitosSource;

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
        //Carrega os volumes salvos
        float volMusica = PlayerPrefabs.GetFloat("volMusica", 1);
        float volEefitos = PlayerPrefabs.GetFloat("volEfeitos", 1);

        if(musicaSource != null){
            musicaSource.volume = volMusica;
        }

        if(efeitosSource != null){
            efeitosSource.volume = volEefitos;
        }

        //Atualiza sliders, se existirem
        if(sliderMusica != null){
            sliderMusica.value = volMusica;
        }

        if(sliderEfeitos != null){
            sliderEfeitos.value = volEefitos;
        }
    }

    //Chamado pelo slider de musica
    public void ChangeVolumeMusica()
    {
        if(musicaSource != null){
            musicaSource.volume = sliderMusica.value;
            PlayerPrefabs.SetFloat("volMusica", sliderMusica.value);
        }
    }

    //Chamado pelo slider de efeitos
    public void ChangeVolumeEfeitos()
    {
        if(efeitosSource != null){
            efeitosSource.volume = sliderEfeitos.value;
            PlayerPrefabs.SetFloat("volEfeitos", sliderEfeitos.value);
        }
    }


    // Efeitos Sonoros
    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayAudioMoeda()
    {
        audioSource.PlayOneShot(coinSound);
    }

    public void PlayAudioPotion()
    {
        audioSource.PlayOneShot(potionSound);
    }

}
