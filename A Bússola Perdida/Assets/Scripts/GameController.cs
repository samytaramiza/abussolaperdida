using UnityEngine; 
using TMPro; 
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    //VIDA DO JOGADOR
    public BarraDeVida barra; //Referência à barra de vida na UI
    private float vida = 100; //Vida atual do jogador

    //PONTUAÇÃO DE POÇÕES
    public int totalScorePocoes; //Total de poções coletadas
    public TMP_Text scoreTextPocoes; //Texto UI que exibe o total de poções

    //PONTUAÇÃO DE ROSAS
    public int totalScoreRosas; //Total de “rosas dos ventos” (ou moedas)
    public TMP_Text scoreTextRosas; //Texto UI que exibe o total de rosas

    //SINGLETON
    //Facilita o acesso global: GameController.instance
    public static GameController instance;

    //ELEMENTOS DE INTERFACE
    public GameObject gameOver; //Painel de Game Over
    public GameObject imagePocao; //Ícone da poção (UI)
    public GameObject imageRosa; //Ícone da rosa dos ventos (UI)
    public GameObject barraDeVida; //Objeto da barra de vida (UI)


    void Awake()
    {
        //Define a instância global ao iniciar
        instance = this;
    }

    void Start()
    {
        //Inicializa a barra de vida no início do jogo
        barra.SetMaxVida(100);
    }

    //ATUALIZAÇÃO DE PONTUAÇÃO
    public void UpdateScoreText()
    {
        //Converte os valores numéricos em texto para exibir na UI
        scoreTextPocoes.text = totalScorePocoes.ToString();
        scoreTextRosas.text = totalScoreRosas.ToString();
    }

    //GAME OVER
    public void ShowGameOver()
    {
        //Ativa o painel de Game Over e oculta os elementos da HUD
        gameOver.SetActive(true);
        imagePocao.SetActive(false);
        imageRosa.SetActive(false);
        barraDeVida.SetActive(false);
    }

    //REINICIAR FASE
    public void Reiniciar(string faseNome)
    {
        //Recarrega a cena atual ou uma cena específica
        SceneManager.LoadScene(faseNome);
    }

    //ALTERAÇÃO DE VIDA
    public void AlterarVida(float quantidade)
    {
        //Aumenta ou reduz a vida, garantindo que fique entre 0 e 100
        vida = Mathf.Clamp(vida + quantidade, 0f, 100f);

        //Atualiza a barra de vida visualmente
        barra.GerenciarVida(vida);

        //Se a vida acabar, exibe Game Over
        if (vida <= 0)
        {
            ShowGameOver();
        }
    }

    //ADICIONAR ROSA (OU MOEDA)
    public void AddRosa(int quantidade)
    {
        //Soma a quantidade coletada
        totalScoreRosas += quantidade;

        //Atualiza o texto da UI
        UpdateScoreText();

        //A cada 10 rosas coletadas, recupera 10 pontos de vida (até o máximo de 100)
        if (totalScoreRosas % 10 == 0 && vida < 100)
        {
            AlterarVida(10f);
        }
    }

    void Update()
    {
        //Espaço para futuras lógicas de atualização contínua
    }
}
