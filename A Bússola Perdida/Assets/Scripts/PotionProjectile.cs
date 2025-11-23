using UnityEngine;

public class PotionProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public int dano = 20;

    [HideInInspector] 
    public int direcao = 1;  // 1 = direita / -1 = esquerda

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Impede cair para o chão
        rb.gravityScale = 0;

        // MOVIMENTO
        rb.linearVelocity = new Vector2(speed * direcao, 0);

        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Boss"))
        {
            EnemyHealth vida = col.gameObject.GetComponent<EnemyHealth>();
            if (vida != null)
                vida.LevarDano(dano);
        }

        Destroy(gameObject);
    }
}
