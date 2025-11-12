using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using UnityEngine;

public class PontoPasseLevel : MonoBehaviour
{
    //Próxima cena
    //public string menuFases;

    //Define se esta é a última fase do jogo.
    //Se for TRUE, ao chegar aqui, o jogador vence o jogo.
    public bool ultimaFase = false;

    //Referência ao painel (Canvas) que exibe a mensagem de vitória.
    public GameObject PanelVictory; 

    //Referências aos objetos de pontuação na tela.
    public GameObject scorePocao;
    public GameObject scoreRosa;

    //Referências às imagens dos ícones de poção e rosa.
    public Image imgPocao;
    public Image imgRosa;

    //Este método é chamado automaticamente quando outro objeto com Collider2D entra
    //na área marcada como "Is Trigger" deste GameObject.
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Verifica se o objeto que entrou no trigger é o jogador (tag "Player").
        if (collider.CompareTag("Player"))
        {
            //Se for a última fase...
            if (ultimaFase)
            {
                //Mostra o painel de vitória (se tiver sido atribuído no Inspector).
                if (PanelVictory != null)
                {
                    PanelVictory.SetActive(true);
                }

                //Esconde as imagens da poção e da rosa, se existirem.
                if (imgPocao != null && imgRosa != null)
                {
                    imgPocao.enabled = false;
                    imgRosa.enabled = false;
                }

                //Desativa os objetos de pontuação (texto, ícones, etc.), se existirem.
                if (scorePocao && scoreRosa != null)
                {
                    scorePocao.SetActive(false);
                    scoreRosa.SetActive(false);
                }

                //Pausa o jogo completamente para que o jogador veja a mensagem de vitória.
                Time.timeScale = 0f;
            }
            else
            {
                //Sistema de desbloqueio de fase
                // Recupera a fase atual liberada (por padrão é 1)
                int faseLiberada = PlayerPrefs.GetInt("faseLiberada", 1);

                //Se a próxima fase ainda não estiver liberada, libera agora
                //(evita liberar além da 4ª fase)
                if (faseLiberada < 4)
                {
                    PlayerPrefs.SetInt("faseLiberada", faseLiberada + 1);
                    PlayerPrefs.Save();
                }

                //Depois de salvar o progresso, carrega a próxima fase
                SceneManager.LoadScene("menuFases");
            }
        }
    }
}
