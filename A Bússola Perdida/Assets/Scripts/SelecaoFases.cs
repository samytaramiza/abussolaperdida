using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelecaoFases : MonoBehaviour
{
    [Header("Botões de Fases")]
    public Button buttonFase1;
    public Button buttonFase2;
    public Button buttonFase3;
    public Button buttonFase4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Carrega o progresso salvo: fase mais alta liberada
        int faseLiberada = PlayerPrefs.GetInt("faseLiberada", 1); //padrão = 1

        //Fase 1 sempre liberada
        buttonFase1.interactable = true;
        buttonFase2.interactable = (faseLiberada >= 2);
        buttonFase3.interactable = (faseLiberada >= 3);
        buttonFase4.interactable = (faseLiberada >= 4);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    // Chamado ao clicar em um botão
    public void CarregarFase(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }
}
