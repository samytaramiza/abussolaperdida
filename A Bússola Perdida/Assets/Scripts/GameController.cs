using UnityEngine; 
using TMPro; 
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int chancesRestantes = 3;

    public BarraDeVida barra;
    private float vida = 100;

    public int totalScorePocoes;
    public TMP_Text scoreTextPocoes;

    public int totalScoreRosas;
    public TMP_Text scoreTextRosas;

    public static GameController instance;

    public GameObject gameOver;
    public GameObject imagePocao;
    public GameObject imageRosa;
    public GameObject barraDeVida;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        barra.SetMaxVida(100);
    }

    public void UpdateScoreText()
    {
        scoreTextPocoes.text = totalScorePocoes.ToString();
        scoreTextRosas.text = totalScoreRosas.ToString();
    }

    // -------- PERDER VIDA --------
    public void AlterarVida(float quantidade)
    {
        vida = Mathf.Clamp(vida + quantidade, 0f, 100f);
        barra.GerenciarVida(vida);

        if (vida <= 0)
        {
            MorteDoPlayer();
        }
    }

    // -------- SISTEMA DE VIDAS --------
    void MorteDoPlayer()
    {
        chancesRestantes--;

        if (chancesRestantes <= 0)
        {
            // AGORA SIM!!! APARECE O GAME OVER
            ShowGameOver();
        }
        else
        {
            // Reinicia fase normalmente
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        imagePocao.SetActive(false);
        imageRosa.SetActive(false);
        barraDeVida.SetActive(false);

        // Garanta que o tempo continue normal
        Time.timeScale = 1f;
    }

    public void AddRosa(int quantidade)
    {
        totalScoreRosas += quantidade;
        UpdateScoreText();

        if (totalScoreRosas % 10 == 0 && vida < 100)
        {
            AlterarVida(10f);
        }
    }
}
