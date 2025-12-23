using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public GameObject panelConfig;
    public GameObject imagePocao;
    public GameObject imageRosa;
    public GameObject barraDeVida;

    void Start()
    {
        panelConfig.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseManager.Instance.TogglePause();

            bool pausado = PauseManager.Instance.IsPaused;

            panelConfig.SetActive(pausado);
            imagePocao.SetActive(!pausado);
            imageRosa.SetActive(!pausado);
            barraDeVida.SetActive(!pausado);
        }
    }

    public void OnVoltarButton()
    {
        PauseManager.Instance.UnPause();

        panelConfig.SetActive(false);
        imagePocao.SetActive(true);
        imageRosa.SetActive(true);
        barraDeVida.SetActive(true);
    }
}
