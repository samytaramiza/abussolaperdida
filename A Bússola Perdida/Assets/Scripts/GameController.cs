using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Player player; 
     public static GameController instance;

    [Header("Sistema de Vida")]
    public BarraDeVida barra;
    private float vida = 100f;
    public int chancesRestantes = 3;

    [Header("Pontuação")]
    public int totalRosas;
    public TMP_Text textoRosas; // formato: "0/30"

    [Header("Game Over UI")]
    public GameObject painelGameOver;
    public GameObject uiVida;
    public GameObject uiRosas;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        barra.SetMaxVida(100);
        AtualizarTextoRosas();
    }

    //--------- VIDA -----------
    public void AlterarVida(float quantidade)
    {
        vida = Mathf.Clamp(vida + quantidade, 0f, 100f);
        barra.GerenciarVida(vida);

        if (vida <= 0)
        {
            MorteDoPlayer();
        }
    }

    //-------- MORTE / CHANCES --------
    void MorteDoPlayer()
    {
        chancesRestantes--;

        if (chancesRestantes <= 0)
        {
            MostrarGameOver();
        }
        else
        {
            Scene cena = SceneManager.GetActiveScene();
            SceneManager.LoadScene(cena.name);
        }
    }

    public void MostrarGameOver()
    {
        Time.timeScale = 1f;

        painelGameOver.SetActive(true);
        uiVida.SetActive(false);
        uiRosas.SetActive(false);
    }

    //--------- ROSAS ----------
    public void AddRosa(int quantidade)
    {
        totalRosas += quantidade;
        AtualizarTextoRosas();

        if (totalRosas >= 30)
        {
            AlterarVida(20f);

            totalRosas = 0;
            AtualizarTextoRosas();
        }
    }

    public void AtualizarTextoRosas()
    {
        textoRosas.text = totalRosas + "/30";
    }

    //--------- ABISMO ----------
    public void PlayerCaiuNoAbismo()
    {
        vida = 0;
        MorteDoPlayer();
    }

}
