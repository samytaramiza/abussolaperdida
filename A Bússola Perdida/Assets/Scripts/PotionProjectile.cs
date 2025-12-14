using UnityEngine;

public class PotionProjectile : MonoBehaviour
{
    public float speed = 8f;
    public int direcao = 1;
    public float dano = 20f;

    void Update()
    {
        transform.Translate(Vector2.right * direcao * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            BossHealth vidaBoss = collision.GetComponent<BossHealth>();
            if (vidaBoss != null)
            {
                vidaBoss.TomarDano(dano);
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("Parede"))
        {
            Destroy(gameObject);
        }
    }
}
