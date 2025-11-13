using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelUnlocker : MonoBehaviour
{
    [Serializable]
    public class Fase
    {
        public Button botao;
        public GameObject cadeado;
        public string nomeCena;
        public int indice;
    }

    public Fase[] fases;
    private const string SAVE_KEY = "faseLiberada";

    void Start()
    {
        // Atualiza após breve atraso, garantindo que o PlayerPrefs já tenha sido salvo
        Invoke(nameof(AtualizarFases), 0.1f);
    }

    public void AtualizarFases()
    {
        int ultimaFaseLiberada = PlayerPrefs.GetInt(SAVE_KEY, 1);
        Debug.Log($"[LevelUnlocker] ultimaFaseLiberada = {ultimaFaseLiberada}");

        foreach (var fase in fases)
        {
            bool desbloqueada = fase.indice <= ultimaFaseLiberada;

            if (fase.cadeado != null)
                fase.cadeado.SetActive(!desbloqueada); // Cadeado visível se bloqueada

            if (fase.botao != null)
            {
                fase.botao.interactable = desbloqueada;

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
}
