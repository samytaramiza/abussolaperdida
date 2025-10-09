using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject panelConfig;// painel de configurações
    private bool isPause; // controla o estado de pausa

    void Start()
    {
        // garantir que o painel comece desligado
        panelConfig.SetActive(false);
    }

    void Pause()
    {
        Time.timeScale = 0; // congela o jogo
        panelConfig.SetActive(true); // mostra painel
        Cursor.lockState = CursorLockMode.None; // libera mouse
        Cursor.visible = true;
    }

    void UnPause()
    {
        Time.timeScale = 1; // volta o tempo
        panelConfig.SetActive(false); // esconde painel
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

    //Esse método será chamado pelo botão "Voltar"
    public void OnVoltarButton()
    {
        UnPause(); // simplesmente despausa e fecha o painel
    }
}
