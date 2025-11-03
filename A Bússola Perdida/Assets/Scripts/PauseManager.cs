using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance; // Singleton

    [Header("Painel de Configurações")]
    public GameObject panelConfig; // painel de configurações

    private bool isPause; // controla o estado de pausa

    void Awake()
    {
        // Garante que só exista um PauseManager na cena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // mantém o objeto entre cenas
        }
        else
        {
            Destroy(gameObject); // evita duplicatas
            return;
        }
    }

    void Start()
    {
        // garantir que o painel comece desligado
        if (panelConfig != null){
            panelConfig.SetActive(false);
        }
    }

    void Pause()
    {
        Time.timeScale = 0; // congela o jogo
        if (panelConfig != null)
            panelConfig.SetActive(true);

        Cursor.lockState = CursorLockMode.None; // libera mouse
        Cursor.visible = true;
    }

    void UnPause()
    {
        Time.timeScale = 1; // volta o tempo
        if (panelConfig != null)
            panelConfig.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked; // trava mouse (opcional)
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;

            if (isPause)
                Pause();
            else
                UnPause();
        }
    }

    // Chamado pelo botão "Voltar"
    public void OnVoltarButton()
    {
        UnPause();
    }
}
