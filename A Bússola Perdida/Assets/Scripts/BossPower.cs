using UnityEngine;

public class PowerBoss : MonoBehaviour
{
    public float speed = 6f;
    public float damage = 10f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerVida vida = other.GetComponent<PlayerVida>();
            if (vida != null)
                vida.TomarDano(damage);

            Destroy(gameObject);
        }
    }
}
