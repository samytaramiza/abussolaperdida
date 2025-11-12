using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //Instância estática (padrão Singleton)
    //Garante que exista apenas um AudioManager acessível globalmente
    public static AudioManager Instance;

    [Header("Sliders de Volume")]
    // SerializeField] → permite editar no Inspector mesmo sendo privado
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderEfeitos;

    [Header("Efeitos Sonoros")]
    //Clipes de áudio que serão tocados em eventos específicos do jogo
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip potionSound;

    [Header("Fontes de Áudio")]
    //Fontes que reproduzem os sons
    public AudioSource musicaSource;
    public AudioSource efeitosSource;

    void Awake()
    {
        //SINGLETON
        // Garante que só exista um AudioManager ativo entre todas as cenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // mantém o objeto entre mudanças de cena
        }
        else
        {
            Destroy(gameObject); // evita duplicação ao carregar nova cena
            return;
        }
    }

    void Start()
    {
        //Se o efeitosSource não for definido manualmente, tenta pegar o componente
        if (efeitosSource == null)
            efeitosSource = GetComponent<AudioSource>();

        //CARREGA VOLUMES SALVOS
        //Usa PlayerPrefs para guardar as preferências de som entre sessões
        float volMusica = PlayerPrefs.GetFloat("volMusica", 1);
        float volEfeitos = PlayerPrefs.GetFloat("volEfeitos", 1);

        // Aplica os volumes às fontes (caso existam)
        if (musicaSource != null)
            musicaSource.volume = volMusica;

        if (efeitosSource != null)
            efeitosSource.volume = volEfeitos;

        //ATUALIZA SLIDERS
        if (sliderMusica != null)
            sliderMusica.value = volMusica;

        if (sliderEfeitos != null)
            sliderEfeitos.value = volEfeitos;
    }

    //FUNÇÕES PARA OS SLIDERS
    //Atualiza o volume da música e salva a preferência
    public void ChangeVolumeMusica()
    {
        if (musicaSource != null)
        {
            musicaSource.volume = sliderMusica.value;
            PlayerPrefs.SetFloat("volMusica", sliderMusica.value);
        }
    }

    //Atualiza o volume dos efeitos sonoros e salva a preferência
    public void ChangeVolumeEfeitos()
    {
        if (efeitosSource != null)
        {
            efeitosSource.volume = sliderEfeitos.value;
            PlayerPrefs.SetFloat("volEfeitos", sliderEfeitos.value);
        }
    }

    //FUNÇÕES DE REPRODUÇÃO DE SOM
    //Usadas por outros scripts ao chamar: AudioManager.Instance.PlayJump();

    public void PlayJump()
    {
        efeitosSource.PlayOneShot(jumpSound); //reproduz o som de pulo
    }

    public void PlayAudioCoin()
    {
        efeitosSource.PlayOneShot(coinSound); //som de pegar moeda
    }

    public void PlayAudioPotion()
    {
        efeitosSource.PlayOneShot(potionSound); //som de pegar poção
    }
}
