using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    [Header("Vida do Boss")]
    public float vidaMaxima = 100f;
    public float vidaAtual;

    [Header("Feedback de Dano")]
    public Color corDeDano = Color.red;
    public float tempoPiscando = 0.15f;

    [Header("Barreira a ser desbloqueada")]
    public GameObject paredeInvisivel; // arraste a barreira no Inspector

    private SpriteRenderer sprite;
    private Color corOriginal;
    private bool tomandoDano;

    void Start()
    {
        vidaAtual = vidaMaxima;

        sprite = GetComponent<SpriteRenderer>();
        corOriginal = sprite.color;
    }

    // ---------------- TOMAR DANO ----------------
    public void TomarDano(float dano)
    {
        if (vidaAtual <= 0) return;

        vidaAtual -= dano;

        // Feedback visual
        if (!tomandoDano)
        {
            StartCoroutine(PiscarDano());
        }

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    // ---------------- PISCAR COR ----------------
    IEnumerator PiscarDano()
    {
        tomandoDano = true;

        sprite.color = corDeDano;

        yield return new WaitForSeconds(tempoPiscando);

        sprite.color = corOriginal;

        tomandoDano = false;
    }

    // ---------------- MORTE ----------------
    void Morrer()
    {
        Debug.Log("Boss morreu!");

        // Desativa a barreira
        if (paredeInvisivel != null)
            paredeInvisivel.SetActive(false);

        // Desativa o boss
        gameObject.SetActive(false);
    }
}
