using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sliders de Volume")]
    //SerializeFiel - se usa quando o atribute deve ser privado, mas aparacer no editor
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderEfeitos;

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

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (efeitosSource == null){
            efeitosSource = GetComponent<AudioSource>();
        }
               
        //Carrega os volumes salvos
        float volMusica = PlayerPrefs.GetFloat("volMusica", 1);
        float volEfeitos = PlayerPrefs.GetFloat("volEfeitos", 1);


        if(musicaSource != null){
            musicaSource.volume = volMusica;
        }

        if(efeitosSource != null){
            efeitosSource.volume = volEfeitos;
        }

        //Atualiza sliders, se existirem
        if(sliderMusica != null){
            sliderMusica.value = volMusica;
        }

        if(sliderEfeitos != null){
            sliderEfeitos.value = volEfeitos;
        }
    }

    //Chamado pelo slider de musica
    public void ChangeVolumeMusica()
    {
        if(musicaSource != null){
            musicaSource.volume = sliderMusica.value;
            PlayerPrefs.SetFloat("volMusica", sliderMusica.value);
        }
    }

    //Chamado pelo slider de efeitos
    public void ChangeVolumeEfeitos()
    {
        if(efeitosSource != null){
            efeitosSource.volume = sliderEfeitos.value;
            PlayerPrefs.SetFloat("volEfeitos", sliderEfeitos.value);
        }
    }


    // Efeitos Sonoros
    public void PlayJump()
    {
        efeitosSource.PlayOneShot(jumpSound);
    }

    public void PlayAudioMoeda()
    {
        efeitosSource.PlayOneShot(coinSound);
    }

    public void PlayAudioPotion()
    {
        efeitosSource.PlayOneShot(potionSound);
    }

}
