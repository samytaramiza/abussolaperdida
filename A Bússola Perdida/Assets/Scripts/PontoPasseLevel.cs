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

    // Índice da fase atual (1 = fase1, 2 = fase2, etc.)
    public int faseAtual = 1;

    // Nome da cena do menu de fases 
    public string menuFasesSceneName = "MenuFases";

    // Quantidade total de fases do jogo
    public int totalFases = 4;

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

        // Recupera fase atual liberada
        int faseLiberada = PlayerPrefs.GetInt(SAVE_KEY, 1);
        Debug.Log($"[PontoPasseLevel] faseLiberada atual = {faseLiberada}");

        // Se o jogador terminou esta fase, libera a próxima
        if (faseLiberada == faseAtual && faseAtual < totalFases)
        {
            int nova = faseAtual + 1;
            PlayerPrefs.SetInt(SAVE_KEY, nova);
            PlayerPrefs.Save();
            Debug.Log($"[PontoPasseLevel] Nova fase liberada: {nova}");
        }

        // Volta ao menu de fases
        SceneManager.LoadScene(menuFasesSceneName);
    }
}
