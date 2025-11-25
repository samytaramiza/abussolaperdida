using UnityEngine;

public class PotionProjectile : MonoBehaviour
{
    public float speed = 8f;
    public int direcao = 1;
    public float dano = 20f; // dano ao boss

    void Update()
    {
        transform.Translate(Vector2.right * direcao * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Dano no Boss
        if (collision.CompareTag("Boss"))
        {
            BossHealth vidaBoss = collision.GetComponent<BossHealth>();
            if (vidaBoss != null)
            {
                vidaBoss.LevarDano(dano);
            }

            Destroy(gameObject);
        }

        // Destruir ao bater em parede
        if (collision.CompareTag("Parede"))
        {
            Destroy(gameObject);
        }
    }
}
