using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance; //Implementa o padrão Singleton (permite acesso global)

    public GameObject imagePocao;
    public GameObject imageRosa;
    public GameObject barraDeVida;

    [Header("Painel de Configurações")]
    public GameObject panelConfig; //Referência ao painel de configurações (Canvas que aparece quando pausa)

    private bool isPause; //Controla se o jogo está pausado (true) ou não (false)

    void Awake()
    {
        // Garante que exista apenas uma instância de PauseManager na cena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Mantém o objeto entre mudanças de cena
        }
        else
        {
            Destroy(gameObject); //Evita duplicatas quando uma nova cena carrega
            return;
        }
    }

    void Start()
    {
        //Garante que o painel de configurações comece desativado
        if (panelConfig != null)
        {
            panelConfig.SetActive(false);
        }
    }

    //Método responsável por PAUSAR o jogo
    void Pause()
    {
        Time.timeScale = 0; //Congela todo o tempo do jogo (paralisa movimentos, animações, etc.)

        //Ativa o painel de configurações, se existir
        if (panelConfig != null)
            panelConfig.SetActive(true);

        //Libera o cursor para o jogador poder usar o menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Método responsável por DESPAUSAR o jogo
    void UnPause()
    {
        Time.timeScale = 1; //Retorna o tempo do jogo ao normal

        //Esconde o painel de configurações, se existir
        if (panelConfig != null)
            panelConfig.SetActive(false);

        //Opcional: trava o cursor novamente (caso o jogo use controle de câmera, FPS, etc.)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Detecta quando o jogador aperta uma tecla para pausar/despausar
    void Update()
    {
        //Se o jogador pressionar "P" ou "Esc"
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            imagePocao.SetActive(false);
            imageRosa.SetActive(false);
            barraDeVida.SetActive(false);

            //Inverte o estado da pausa (toggle)
            isPause = !isPause;

            //Se estiver pausado, chama Pause(); senão, UnPause()
            if (isPause)
                Pause();
            else
                UnPause();
        }
    }

    //Método chamado pelo botão "Voltar" no painel de pausa/configuração
    public void OnVoltarButton()
    {
        UnPause(); // Retoma o jogo
    }
}
