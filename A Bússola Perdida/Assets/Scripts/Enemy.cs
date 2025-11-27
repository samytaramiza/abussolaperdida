using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float dano = 20f;         // Dano que dá no player pelo lado
    public GameObject deathEffect;   // Efeito ao morrer (opcional)

    private bool morreu = false;     // Evita morrer duas vezes

    void OnCollisionEnter2D(Collision2D colisao)
    {
        // Se já morreu, ignora
        if (morreu) return;

        // Pegamos o ponto de contato
        ContactPoint2D contato = colisao.GetContact(0);

        // ---------------------------------------------------------
        // 1️⃣ PLAYER PISOU EM CIMA DO DUENDE → INIMIGO MORRE
        // ---------------------------------------------------------
        if (colisao.collider.CompareTag("Player"))
        {
            // Se o player está ACIMA do duende (pisou)
            if (contato.normal.y < -0.5f)
            {
                Morrer();
                return;
            }
            else
            {
                // ---------------------------------------------------------
                // 2️⃣ PLAYER TOCOU DE LADO → DUENDE NÃO MORRE
                // Só dá dano no player
                // ---------------------------------------------------------
                GameController.instance.AlterarVida(-dano);
            }
        }
    }

    void Morrer()
    {
        morreu = true;

        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}