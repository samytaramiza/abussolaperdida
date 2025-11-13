using UnityEngine;
using UnityEngine.SceneManagement;

public class PontoPasseLevel : MonoBehaviour
{
    private const string SAVE_KEY = "faseLiberada"; 
    public bool ultimaFase = false;

    public GameObject PanelVictory;
    public GameObject scorePocao;
    public GameObject scoreRosa;
    public UnityEngine.UI.Image imgPocao;
    public UnityEngine.UI.Image imgRosa;

    public int faseAtual = 1;
    public int totalFases = 4;
    public string menuFasesSceneName = "MenuFases";

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;

        if (ultimaFase)
        {
            if (PanelVictory != null) PanelVictory.SetActive(true);
            if (imgPocao != null && imgRosa != null) { imgPocao.enabled = false; imgRosa.enabled = false; }
            if (scorePocao != null && scoreRosa != null) { scorePocao.SetActive(false); scoreRosa.SetActive(false); }
            Time.timeScale = 0f;
            Debug.Log("[PontoPasseLevel] Última fase concluída!");
            return;
        }

        int faseLiberada = PlayerPrefs.GetInt(SAVE_KEY, 1);
        Debug.Log($"[PontoPasseLevel] Fase liberada atual = {faseLiberada}");

        // libera a próxima fase apenas se o jogador estiver na mais recente
        if (faseLiberada == faseAtual && faseAtual < totalFases)
        {
            int nova = faseAtual + 1;
            PlayerPrefs.SetInt(SAVE_KEY, nova);
            PlayerPrefs.Save();
            Debug.Log($"[PontoPasseLevel] Nova fase liberada: {nova}");
        }

        // Garante que o valor seja gravado antes da troca de cena
        StartCoroutine(VoltarMenu());
    }

    private System.Collections.IEnumerator VoltarMenu()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(menuFasesSceneName);
    }
}
