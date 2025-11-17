using UnityEngine;

public class BossPower : MonoBehaviour
{
    [Header("Configurações")]
    public float lifeTime = 3f;      // Tempo de vida do projétil
    public float damage = 10f;       // Dano causado ao jogador

    [Header("Efeito de Impacto")]
    public GameObject hitEffect;     // Efeito ao colidir (opcional)

    void Start()
    {
        // Destroi automaticamente depois de lifeTime
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Se colidir com o jogador
        if (collision.CompareTag("Player"))
        {
            // Se o jogador tiver script de vida, aplica dano
            PlayerVida vida = collision.GetComponent<PlayerVida>();
            if (vida != null)
                vida.TomarDano(damage);
        }

        // Se houver efeito de impacto, instancie
        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        // Destrói o projétil ao colidir
        Destroy(gameObject);
    }
}
