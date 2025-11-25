using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float vida = 20f;
    public GameObject deathParticles; // particulas de desintegração

    public void LevarDano(float dano)
    {
        vida -= dano;

        if (vida <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        // Instancia as partículas
        if (deathParticles != null)
            Instantiate(deathParticles, transform.position, Quaternion.identity);

        Destroy(gameObject); // destrói o inimigo
    }
}
