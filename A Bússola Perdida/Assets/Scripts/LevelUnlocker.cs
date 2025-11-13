using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelUnlocker : MonoBehaviour
{
    [Serializable]
    public class Fase
    {
        public Button botao;       // botão da fase (com imagem)
        public GameObject cadeado; // ícone do cadeado (GameObject)
        public string nomeCena;    // nome da cena para carregar
        public int indice;         // 1,2,3...
    }

    public Fase[] fases;
    private const string SAVE_KEY = "faseLiberada";

    void Start()
    {
        AtualizarFases();
        PlayerPrefs.DeleteAll();
    }

    public void AtualizarFases()
    {
        int ultimaFaseLiberada = PlayerPrefs.GetInt(SAVE_KEY, 1);
        Debug.Log($"[LevelUnlocker] ultimaFaseLiberada = {ultimaFaseLiberada}");

        foreach (var fase in fases)
        {
            bool desbloqueada = fase.indice <= ultimaFaseLiberada;

            if (fase.cadeado != null)
                fase.cadeado.SetActive(!desbloqueada);

            if (fase.botao != null)
            {
                fase.botao.interactable = desbloqueada;

                // Remove listeners antigos (evita acumular multiplos listeners)
                fase.botao.onClick.RemoveAllListeners();

                if (desbloqueada)
                {
                    string cena = fase.nomeCena;
                    fase.botao.onClick.AddListener(() =>
                    {
                        Debug.Log($"[LevelUnlocker] Carregando cena: {cena}");
                        SceneManager.LoadScene(cena);
                    });
                }
            }
        }
    }

    // Método público para desbloquear até uma fase específica (ex: chamar ao fim da fase)
    public static void DesbloquearAte(int indice)
    {
        int atual = PlayerPrefs.GetInt(SAVE_KEY, 1);
        if (indice > atual)
        {
            PlayerPrefs.SetInt(SAVE_KEY, indice);
            PlayerPrefs.Save();
            Debug.Log($"[LevelUnlocker] PlayerPrefs atualizado: faseLiberada = {indice}");
        }
    }
}
