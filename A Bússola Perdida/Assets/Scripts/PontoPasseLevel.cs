using UnityEngine.SceneManagement; // Necessária para trocar de cenas no jogo
using UnityEngine.UI;
using UnityEngine; // Biblioteca principal do Unity (acesso a componentes, objetos, etc.)


public class PontoPasseLevel : MonoBehaviour
{
    //
    public string nextLvlName;

    // Definida como TRUE na última fase
    public bool ultimaFase = false;

    // Referência ao painel/canvas com a mensagem de vitória
    public GameObject PanelVictory; 
    public GameObject scorePocao;
    public GameObject scoreRosa;

    public Image imgPocao;
    public Image imgRosa;

    // Chamado automaticamente quando outro Collider2D entra na área marcada como "Is Trigger"
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Verifica se o objeto que entrou no trigger tem a tag "Player"
        if (collider.CompareTag("Player"))
        {
            // Se esta for a última fase
            if (ultimaFase)
            {
                // Ativa o painel de vitória, se ele estiver configurado no Inspector
                if (PanelVictory != null){
                    PanelVictory.SetActive(true);
                    
                }

                if(imgPocao != null && imgRosa != null){
                    imgPocao.enabled = false;
                    imgRosa.enabled = false;
                }

                if(scorePocao && scoreRosa != null){
                    scorePocao.SetActive(false);
                    scoreRosa.SetActive(false);
                }

                // Pausa o jogo para o jogador poder ver a mensagem sem que a ação continue
                Time.timeScale = 0f;
            }
            else
            {
                // Caso não seja a última fase, carrega a próxima cena
                SceneManager.LoadScene(nextLvlName);
            }
        }
    }
}
