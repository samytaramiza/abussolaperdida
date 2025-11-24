using UnityEngine;

public class BossPower : MonoBehaviour
{
    public float lifeTime = 3f;
    public float damage = 10f;
    public GameObject hitEffect;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // aplica dano
            GameController.instance.AlterarVida(-damage);
        }

        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
