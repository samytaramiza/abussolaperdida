using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Player player;

    [Header("Sistema de Vida")]
    public BarraDeVida barra;
    private float vida = 100f;
    public int chancesRestantes = 3;

    [Header("Rosas")]
    public int totalRosas;
    public TMP_Text textoRosas;

    [Header("Poções")]
    public int totalPocao;
    public TMP_Text textoPocao; // texto que começa em "0"

    [Header("Game Over UI")]
    public GameObject painelGameOver;
    public GameObject uiVida;
    public GameObject uiRosas;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        barra.SetMaxVida(100);
        AtualizarTextoRosas();
        AtualizarTextoPocao();
    }

    // ---------------- VIDA ----------------
    public void AlterarVida(float quantidade)
    {
        vida = Mathf.Clamp(vida + quantidade, 0f, 100f);
        barra.GerenciarVida(vida);

        if (vida <= 0)
            MorteDoPlayer();
    }

    void MorteDoPlayer()
    {
        chancesRestantes--;

        if (chancesRestantes <= 0)
            MostrarGameOver();
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MostrarGameOver()
    {
        Time.timeScale = 1f;
        painelGameOver.SetActive(true);
        uiVida.SetActive(false);
        uiRosas.SetActive(false);
    }

    // ---------------- ROSAS ----------------
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

    void AtualizarTextoRosas()
    {
        textoRosas.text = totalRosas + "/30";
    }

    // ---------------- POÇÕES ----------------
    public void AddPocao(int quantidade)
    {
        totalPocao += quantidade;
        AtualizarTextoPocao();
    }

    public bool TemPocao()
    {
        return totalPocao > 0;
    }

    public void UsarPocao()
    {
        if (totalPocao <= 0) return;

        totalPocao--;
        AtualizarTextoPocao();
    }

    void AtualizarTextoPocao()
    {
        textoPocao.text = totalPocao.ToString();
    }

    // ---------------- ABISMO ----------------
    public void PlayerCaiuNoAbismo()
    {
        vida = 0;
        MorteDoPlayer();
    }
}
